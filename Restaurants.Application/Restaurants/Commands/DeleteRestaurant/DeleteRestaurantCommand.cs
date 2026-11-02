using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand(int id):IRequest
    {
        public int Id { get; } = id;
    }
}
