using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing {UserEmail} to opearte {Operation} for restaurant: {RestaurantName}", user.Email, resourceOperation, restaurant.Name);
            if(resourceOperation==ResourceOperation.Create||resourceOperation==ResourceOperation.Read)
            {
                logger.LogInformation("Create/Read operation authorized succefully");
                return true;
            }
            if(resourceOperation==ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Delete operation authorized succefully for admin");
                return true;
            }
            if((resourceOperation==ResourceOperation.Update||resourceOperation==ResourceOperation.Delete)&&restaurant.OwnerId==user.Id)
            {
                logger.LogInformation("owner's restaurant authorized succefully");
                return true;
            }
            return false;
        }
    }
}
