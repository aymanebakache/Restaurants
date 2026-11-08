using System;
using System.Collections.Generic;
using System.Text;
namespace Restaurants.Application.Users
{
    public record CurrentUser(string Id, string Email, IEnumerable<string> Roles,string? Nationality,DateOnly? BirthDay)
    {
        public bool IsInRole(string Role) => Roles.Contains(Role);
    }
}
