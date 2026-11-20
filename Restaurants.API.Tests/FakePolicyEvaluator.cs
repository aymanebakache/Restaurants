using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Restaurants.API.Tests
{
    internal class FakePolicyEvaluator : IPolicyEvaluator
    {
        Task<AuthenticateResult> IPolicyEvaluator.AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
            new[]
            {
            new Claim(ClaimTypes.NameIdentifier,"1"),
            new Claim(ClaimTypes.Role,"Admin"),
            }
            ));
            var ticket = new AuthenticationTicket(claimsPrincipal, "Test");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }

        Task<PolicyAuthorizationResult> IPolicyEvaluator.AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            var result = PolicyAuthorizationResult.Success();
            return Task.FromResult(result);
        }
    }
}
