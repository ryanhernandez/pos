using Microsoft.EntityFrameworkCore;

namespace Server.Shared.Infrastructure
{
    public static class EfCoreExtensions
    {
        public static void ConfigureSqlite(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlite(connectionString);
        }
    }
}
