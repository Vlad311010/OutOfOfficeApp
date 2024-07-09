using Microsoft.AspNetCore.Authorization;
using OutOfOfficeWebApp;
using OutOfOfficeWebApp.Utils;

namespace app.Authorization
{
    public class OwnerRequirement : IAuthorizationRequirement { }

    public class OwnerAuthorizationHandler : AuthorizationHandler<OwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement)
        {
            if (context.User.IsInManagerRole())
                context.Succeed(requirement);

            if (context.Resource is OwnedPageAuthorizationRequest resource)
            {
                string identifier = context.User.GetIdentifier();
                if (resource.AllowedUsers.Contains(Int32.Parse(identifier)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

}
