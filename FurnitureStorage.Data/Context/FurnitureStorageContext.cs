using FurnitureStorage.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Data.Context
{
    public class FurnitureStorageContext : DbContext
    {
        public DbSet<Furniture> Furniture => Set<Furniture>();
        public DbSet<Dimensions> Dimensions => Set<Dimensions>();
        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();

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
