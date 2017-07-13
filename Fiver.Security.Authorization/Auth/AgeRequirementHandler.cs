using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fiver.Security.Authorization.Auth
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(
                c => c.Type == ClaimTypes.DateOfBirth).Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (calculatedAge > requirement.Age)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
