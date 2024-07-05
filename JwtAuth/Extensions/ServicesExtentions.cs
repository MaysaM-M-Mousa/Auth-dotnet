using JwtAuth.Middlewares;
using JwtAuth.Services;

namespace JwtAuth.Extensions;

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
