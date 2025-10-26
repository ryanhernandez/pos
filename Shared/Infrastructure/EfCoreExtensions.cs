using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Shared.Infrastructure
{
    public static class EfCoreExtensions
    {
        // Helper to register DbContext with a connection string (optional)
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
            return services;
        }
    }
}
