using Fiver.Security.Authorization.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fiver.Security.Authorization
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", 
                    policy => policy.RequireAuthenticatedUser());

                options.AddPolicy("Member",
                    policty => policty.RequireClaim("MembershipId"));

                options.AddPolicy("PaidMember",
                    policty => policty.RequireClaim("HasCreditCard", "Y"));

                options.AddPolicy("Over18",
                    policy => policy.Requirements.Add(new AgeRequirement(18)));
            });

            services.AddScoped<IAuthorizationHandler, AgeRequirementHandler>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter("Authenticated"));
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AccessDeniedPath = new PathString("/Security/Access"),
                AuthenticationScheme = "FiverSecurityCookie",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/Security/Login")
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
