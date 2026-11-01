using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurant is null)
            return NotFound();
        return Ok(restaurant);
    }
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand createRestaurantCommand,IValidator<CreateRestaurantCommand> validator)
    {
        var result = await validator.ValidateAsync(createRestaurantCommand);
        if (!result.IsValid)
            return BadRequest(result.ToDictionary());
        int id = await mediator.Send(createRestaurantCommand);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
        if (isDeleted)
            return NoContent();
        return NotFound();
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id,UpdateRestaurantCommand updateRestaurantCommand,IValidator<UpdateRestaurantCommand> validator)
    {
        var result = await validator.ValidateAsync(updateRestaurantCommand);
        if(!result.IsValid)
            return BadRequest(result.ToDictionary());
        updateRestaurantCommand.Id = id;
        var isUpdated = await mediator.Send(updateRestaurantCommand);
        if (isUpdated)
            return NoContent();
        return NotFound();
    }
}