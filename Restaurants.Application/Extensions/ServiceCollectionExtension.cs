using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IRestaurantsService, RestaurantService>();
            service.AddAutoMapper(cfg=>cfg.AddMaps(typeof(ServiceCollectionExtension).Assembly));
        }
    }
}
