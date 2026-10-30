using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<IRestaurantsService> logger) : IRestaurantsService
    {
        public Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            logger.LogInformation("Get all Restaurants...!");
            var restaurants = restaurantsRepository.GetAllAsync();
            return restaurants;
        }
    }
}
