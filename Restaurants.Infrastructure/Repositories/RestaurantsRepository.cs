using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext restaurantsDbContext):IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await restaurantsDbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await restaurantsDbContext.Restaurants
                                                       .Include(r=>r.Dishes)
                                                       .FirstOrDefaultAsync(r=>r.Id==id);
            return restaurant;
        }
    }
}
