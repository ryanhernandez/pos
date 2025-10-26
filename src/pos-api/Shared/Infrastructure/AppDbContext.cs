using Microsoft.EntityFrameworkCore;
using Server.Modules.Inventory.Domain;
using Server.Modules.Users.Domain;

namespace Server.Shared.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
                b.Property(x => x.Description).HasMaxLength(1000);
                b.Property(x => x.Price).HasPrecision(18, 4);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Username).IsRequired().HasMaxLength(100);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.DisplayName).HasMaxLength(200);
            });
        }
    }
}
