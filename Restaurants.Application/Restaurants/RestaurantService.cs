using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantService(IRestaurantsRepository restaurantsRepository, ILogger<IRestaurantsService> logger) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Get all Restaurants...!");
            var restaurants = await restaurantsRepository.GetAllAsync();
            var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);
            return restaurantsDto!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Getting restaurant {id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(id);
            var restaurantDto = RestaurantDto.FromEntity(restaurant);
            return restaurantDto;
        }
    }
}
