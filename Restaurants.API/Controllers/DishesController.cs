using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator):ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId,CreateDishCommand createDishCommand,IValidator<CreateDishCommand> validator)
        {
            var result = await validator.ValidateAsync(createDishCommand);
            if (!result.IsValid)
                return BadRequest(result.ToDictionary());
            createDishCommand.RestaurantId = restaurantId;
            await mediator.Send(createDishCommand);
            return Created();
        }
    }
}
