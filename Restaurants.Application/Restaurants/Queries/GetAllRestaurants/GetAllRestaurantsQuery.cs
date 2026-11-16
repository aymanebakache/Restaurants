using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery:IRequest<PagedResult<RestaurantDto>>
    {
        public string? SearchPhrase{ get; set; }
        public int PageSize{ get; set; }
        public int PageNumber{ get; set; }
    }
}
