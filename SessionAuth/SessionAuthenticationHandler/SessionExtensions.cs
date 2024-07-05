using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SessionAuth.SessionAuthenticationHandler;

public static class SessionExtensions
{
    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, Action<SessionAuthenticationOptions> configureOptions)
        => builder.AddSession(SessionAuthenticationDefaults.AuthenticationScheme, SessionAuthenticationDefaults.DisplayName, configureOptions);

    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, string authenticationScheme, Action<SessionAuthenticationOptions> configureOptions)
        => builder.AddSession(authenticationScheme, SessionAuthenticationDefaults.DisplayName, configureOptions);

    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, string authenticationScheme, string? displayName, Action<SessionAuthenticationOptions> configureOptions)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<SessionAuthenticationOptions>, PostConfigureSessionAuthenticationOptions>());
        builder.AddScheme<SessionAuthenticationOptions, SessionAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        return builder;
    }
}
