using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirement> logger,IUserContext userContext):AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,MinimumAgeRequirement ageRequirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("User:{Email}, Date of birth:{Dob}-Handling minimum age requirement", currentUser.Email, currentUser.BirthDay);
            if(currentUser.BirthDay==null)
            {
                logger.LogWarning("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }
            if (currentUser.BirthDay.Value.AddYears(ageRequirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Now))
            {
                logger.LogInformation("Authorization successed");
                context.Succeed(ageRequirement);
            }
            else
                context.Fail();
            return Task.CompletedTask;

        }
    }
}
