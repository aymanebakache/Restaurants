using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}
