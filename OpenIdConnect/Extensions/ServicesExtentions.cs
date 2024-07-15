using OpenIdConnect.Services;

namespace OpenIdConnect.Extensions;

public static class ServicesExtentions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
