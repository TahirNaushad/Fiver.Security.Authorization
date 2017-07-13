using System.Threading.Tasks;
using Fiver.Security.Authorization.Models.Rentals;
using Microsoft.AspNetCore.Authorization;

namespace Fiver.Security.Authorization.Auth
{
    public class RentNewReleaseRequirementHandler :
        AuthorizationHandler<RentNewReleaseRequirement, Rental>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, RentNewReleaseRequirement requirement, 
            Rental resource)
        {
            if (!context.User.HasClaim(c => c.Type == "AllowNewReleases"))
                return Task.CompletedTask;

            var allowNewReleases = bool.Parse(context.User.FindFirst("AllowNewReleases").Value);

            if (resource.IsNewRelease && allowNewReleases)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
