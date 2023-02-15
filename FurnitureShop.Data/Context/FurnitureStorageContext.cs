using FurnitureShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Data.Context
{
    public class FurnitureStorageContext : DbContext
    {
        public DbSet<Furniture> Furniture => Set<Furniture>();
        public DbSet<Dimensions> Dimensions => Set<Dimensions>();
        public DbSet<Order> Orders => Set<Order>();

        public DbSet<ShoppingCartItem> OrderProducts => Set<ShoppingCartItem>();

        public DbSet<User> Users => Set<User>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public FurnitureStorageContext(DbContextOptions<FurnitureStorageContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Dimensions>(builder =>
                {
                    builder.ToTable("Dimensions");
                    builder.HasKey(dimensions => dimensions.FurnitureId);
                });
        }
    }
}
