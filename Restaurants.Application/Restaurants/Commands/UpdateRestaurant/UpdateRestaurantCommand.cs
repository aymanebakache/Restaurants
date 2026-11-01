using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }
    }
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Updating Restaurant with id {request.Id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
                return false;
            mapper.Map(request, restaurant);
            await restaurantsRepository.SaveChanges();
            return true;
        }
    }
}
