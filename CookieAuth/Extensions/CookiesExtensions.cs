using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookieAuth.Extensions;

public static class CookiesExtensions
{
    public static IServiceCollection AddCookiesAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opts =>
            {
                opts.LoginPath = "/home/login";
            });

        return services;
    }
}
