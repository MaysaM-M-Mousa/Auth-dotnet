using SessionAuth.SessionAuthenticationHandler;

namespace SessionAuth.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(SessionAuthenticationDefaults.AuthenticationScheme)
            .AddSession(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
                options.SessionName = "session_id";
            });

        return services;
    }
}
