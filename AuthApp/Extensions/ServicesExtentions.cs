using AuthApp.Middlewares;
using AuthApp.Services;

namespace AuthApp.Extensions;

public static class ServicesExtentions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<AddUserDataMiddleware>();

        return services;
    }
}
