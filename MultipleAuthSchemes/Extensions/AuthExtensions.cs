using Microsoft.AspNetCore.Authentication.Cookies;

namespace MultipleAuthSchemes.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthenticationSchemes(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
            {
                opts.LoginPath = "";
                opts.LogoutPath = "";
            })
            .AddCookie("Cookies2", opts =>
            {
                opts.LoginPath = "";
                opts.LogoutPath = "";
            });

        return services;
    }
}
