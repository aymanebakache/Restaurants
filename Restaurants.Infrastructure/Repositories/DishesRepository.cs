using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext restaurantsDbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            restaurantsDbContext.Dishes.Add(entity);
            await restaurantsDbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
