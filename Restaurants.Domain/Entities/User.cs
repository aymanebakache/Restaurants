using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? BirthDay { get; set; }
        public string? Nationality { get; set; }
    }
}
