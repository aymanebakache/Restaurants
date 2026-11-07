using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnassignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetailsCommand)
        {
            await mediator.Send(updateUserDetailsCommand);
            return NoContent();
        }
        [HttpPost("userRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand assignUserRoleCommand)
        {
            await mediator.Send(assignUserRoleCommand);
            return NoContent();
        }
        [HttpDelete("userRole")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand unassignUserRoleCommand)
        {
            await mediator.Send(unassignUserRoleCommand);
            return NoContent();
        }
    }
}
