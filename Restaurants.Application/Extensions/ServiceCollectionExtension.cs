using FluentValidation;
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
            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            
            service.AddAutoMapper(cfg=>cfg.AddMaps(applicationAssembly));
            service.AddValidatorsFromAssembly(applicationAssembly);
        }
    }
}
