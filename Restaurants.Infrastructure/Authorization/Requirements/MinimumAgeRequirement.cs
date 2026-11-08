using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirement(int minimumAge):IAuthorizationRequirement
    {
        public int MinimumAge { get; set; } = minimumAge;
    }
}
