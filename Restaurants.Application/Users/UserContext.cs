using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace Restaurants.Application.Users
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if(user==null)
            {
                throw new InvalidOperationException("User context is not present");
            }
            if(user.Identity==null || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
            var birthdayString = user.FindFirst(c => c.Type == "BirthDay")?.Value;
            var birthday = birthdayString == null ? (DateOnly?)null : DateOnly.ParseExact(birthdayString, "yyyy-MM-dd");
            return new CurrentUser(userId, userEmail, userRoles,nationality,birthday);
        }
    }
}
