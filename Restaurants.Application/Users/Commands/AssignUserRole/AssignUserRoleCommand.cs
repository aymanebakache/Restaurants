using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
