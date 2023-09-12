using Microsoft.AspNetCore.Authorization;

namespace MultipleAuthSchemes.Extensions;

public static class PoliciesExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(opts =>
        {
            // By default, any endpoint marked with [Authorize] attribute, will be checked for both schemes
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder("Cookies", "Cookies2")
                .RequireAuthenticatedUser();

            opts.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

            opts.AddPolicy("OnlySpecialUsers", policy =>
            {
                policy.AuthenticationSchemes.Add("Cookies2");
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }
}
