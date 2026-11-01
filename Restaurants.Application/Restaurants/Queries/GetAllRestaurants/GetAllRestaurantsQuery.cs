using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery:IRequest<IEnumerable<RestaurantDto>>
    {

    }
}
