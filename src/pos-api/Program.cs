using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Server.Shared.Infrastructure;
using Server.Modules.Inventory;
using Server.Modules.Users;
using Server.Shared.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add DbContext (SQLite)
var connectionString = configuration.GetConnectionString("Sqlite") ?? "Data Source=./data/pos.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Register modules (Inventory and Users)
builder.Services.AddInventoryModule();
builder.Services.AddUsersModule();

// Add GraphQL server. InventoryModule also registers its types but we ensure server is present.
builder.Services.AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddMutationType(d => d.Name("Mutation"))
    .AddType<Server.Shared.GraphQL.DateTimeType>();

// Add REST controllers + Swagger (OpenAPI)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "pos-api", Version = "v1" });
});

var app = builder.Build();

// Ensure DB directory exists and apply migrations at startup (development convenience)
var dbPath = System.IO.Path.GetFullPath(configuration.GetConnectionString("Sqlite")?.Replace("Data Source=", "") ?? "./data/pos.db");
Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dbPath) ?? ".");

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.Migrate();
}

// HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "pos-api v1"));
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGraphQL("/graphql");
});

// Minimal health endpoint for quick checks (keeps previous /health route)
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.Run();
