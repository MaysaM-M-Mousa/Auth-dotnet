﻿using Microsoft.AspNetCore.Authentication;

namespace SessionAuth.SessionAuthenticationHandler;

public static class SessionExtensions
{
    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, Action<SessionAuthenticationOptions> configureOptions)
        => builder.AddSession(SessionAuthenticationDefaults.AuthenticationScheme, SessionAuthenticationDefaults.AuthenticationScheme, configureOptions);

    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, string authenticationScheme, Action<SessionAuthenticationOptions> configureOptions)
        => builder.AddSession(authenticationScheme, authenticationScheme, configureOptions);

    public static AuthenticationBuilder AddSession(this AuthenticationBuilder builder, string authenticationScheme, string? displayName, Action<SessionAuthenticationOptions> configureOptions)
    {
        builder.AddScheme<SessionAuthenticationOptions, SessionAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        return builder;
    }
}
