using Microsoft.AspNetCore.Authentication.Cookies;

namespace OAuth.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddApplicationOAuth(
        this IServiceCollection services,
        IConfiguration configurations)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "oauth"; 
        }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddOAuth("oauth", options =>
        {
            // these are read from user secrets
            options.ClientId = configurations["OAuth:Github:ClientId"];
            options.ClientSecret = configurations["OAuth:Github:ClientSecret"];
            options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
            options.TokenEndpoint = "https://github.com/login/oauth/access_token";
            options.CallbackPath = "/oauth/github-cb";
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.SaveTokens = true;
            options.Scope.Add("repo");
        });

        return services;
    }
}
