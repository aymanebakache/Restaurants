using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using FluentAssertions;
using Xunit;


namespace Restaurants.Application.Tests.Users
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.Owner)]
       // [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            var user = new CurrentUser("1", "a.a@asp.net", [UserRoles.Admin, UserRoles.Owner], null, null);
            //act
            var isInRole = user.IsInRole(roleName);
            //assert
            isInRole.Should().BeTrue();
            
        }
        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange
            var user = new CurrentUser("1", "a.a@asp.net", [UserRoles.Admin, UserRoles.Owner], null, null);
            //act
            var isInRole = user.IsInRole(UserRoles.User);
            //assert
            isInRole.Should().BeFalse();
            
        }
        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            //arrange
            var user = new CurrentUser("1", "a.a@asp.net", [UserRoles.Admin, UserRoles.Owner], null, null);
            //act
            var isInRole = user.IsInRole(UserRoles.Owner.ToLower());
            //assert
            isInRole.Should().BeFalse();
            
        }
    }
}
