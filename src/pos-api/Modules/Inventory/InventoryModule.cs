using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Inventory.Domain;
using Server.Modules.Inventory.Infrastructure;
using Server.Modules.Inventory.GraphQL;

namespace Server.Modules.Inventory
{
    public static class InventoryModule
    {
        // Registers Inventory services, repository and GraphQL types.
        public static IServiceCollection AddInventoryModule(this IServiceCollection services)
        {
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<InventoryService>();

            // Register the GraphQL types for this module. For larger solutions you may prefer
            // a single AddGraphQLServer call in Program.cs and modules to call .AddType<...>()
            services.AddGraphQLServer()
                .AddQueryType<InventoryQueries>()
                .AddMutationType<InventoryMutations>();

            return services;
        }
    }
}
