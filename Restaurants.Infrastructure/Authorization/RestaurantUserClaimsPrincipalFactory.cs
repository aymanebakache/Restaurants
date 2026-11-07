using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantUserClaimsPrincipalFactory(UserManager<User> userManager,
                                                      RoleManager<IdentityRole> roleManager,
                                                      IOptions<IdentityOptions> options)
                                                      :UserClaimsPrincipalFactory<User,IdentityRole>(userManager,roleManager,options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);
            if(user.Nationality!=null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
            }
            if(user.BirthDay!=null)
            {
                id.AddClaim(new Claim(AppClaimTypes.BirthDay, user.BirthDay.Value.ToString("yyyy-MM-dd")));
            }
            return new ClaimsPrincipal(id);
        }
    }
}
