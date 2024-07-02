using Microsoft.AspNetCore.Authentication;

namespace SessionAuth.SessionAuthenticationHandler;

public static class SessionExtensions
{
    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, string authenticationScheme, Action<SessionAuthenticationOptions> configureOptions)
    {
        builder.AddScheme<SessionAuthenticationOptions, InMemorySessionAuthenticationHandler>(authenticationScheme, authenticationScheme, configureOptions);
        return builder;
    }
}
