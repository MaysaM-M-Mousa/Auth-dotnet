using SessionAuth.Services;
using SessionAuth.SessionManager;

namespace SessionAuth.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<ISessionManager, InMemorySessionManager>();

        return services;
    }
}
