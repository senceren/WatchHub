using ApplicationCore.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class WatchHubContext : DbContext
    {
        public WatchHubContext(DbContextOptions<WatchHubContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Basket> Baskets => Set<Basket>();
        public DbSet<BasketItem> BasketItems => Set<BasketItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // çalışan sınıf içinde interfacei IEntityType olanları tarayayarak algılıyor.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // tek tek bu şekilde de ekleyebiliriz.
            //modelBuilder.ApplyConfiguration(new BrandConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new CategoryConfiguration());

        }

    }
}
