using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantsDbContext:DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RestaurantsDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>()
                        .OwnsOne(r => r.Address);
            modelBuilder.Entity<Restaurant>()
                        .HasMany(r => r.Dishes)
                        .WithOne()
                        .HasForeignKey(d => d.RestaurantId);
        }
    }
}
