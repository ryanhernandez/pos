using Microsoft.EntityFrameworkCore;
using Server.Shared.Infrastructure;
using Server.Modules.Inventory;
using Server.Modules.Inventory.Domain;
using Server.Shared.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add DbContext (SQLite)
var connectionString = configuration.GetConnectionString("Sqlite") ?? "Data Source=./data/pos.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Register modules (Inventory as example)
builder.Services.AddInventoryModule();

// Add GraphQL server. InventoryModule also registers its types but we ensure server is present.
builder.Services.AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddMutationType(d => d.Name("Mutation"))
    .AddType<DateTimeType>();

var app = builder.Build();

// Ensure DB directory exists and apply migrations at startup (development convenience)
var dbPath = Path.GetFullPath(configuration.GetConnectionString("Sqlite")?.Replace("Data Source=", "") ?? "./data/pos.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? ".");

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.Migrate();
}

// Map endpoints
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));
app.MapGraphQL("/graphql");

app.Run();
