using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommand(int restaurantId):IRequest
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
