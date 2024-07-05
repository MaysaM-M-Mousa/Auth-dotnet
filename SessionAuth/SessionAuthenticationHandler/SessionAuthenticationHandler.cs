using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SessionAuth.SessionManager;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SessionAuth.SessionAuthenticationHandler;

public class SessionAuthenticationHandler : SignInAuthenticationHandler<SessionAuthenticationOptions>
{
    private readonly ISessionManager _sessionManager;
    
    public SessionAuthenticationHandler(
        IOptionsMonitor<SessionAuthenticationOptions> options,
        ILoggerFactory logger, UrlEncoder encoder,
        ISystemClock clock,
        ISessionManager sessionManager)
        : base(options, logger, encoder, clock)
    {
        _sessionManager = sessionManager;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        var userId = user.FindFirstValue("Id");
        var expirationTime = DateTime.UtcNow + Options.ExpireTimeSpan.GetValueOrDefault();

        var session = await _sessionManager.CreateSession(Guid.Parse(userId), expirationTime);

        Options
            .CookieManager
            .AppendResponseCookie(Context, Options.SessionName, session.Value, new());
    }

    protected override async Task HandleSignOutAsync(AuthenticationProperties? properties)
    {
        var sessionValue = Options.CookieManager.GetRequestCookie(Context, Options.SessionName); ;

        if (string.IsNullOrEmpty(sessionValue))
        {
            throw new Exception($"There is no corrosponding session value for the key {Options.SessionName}");
        }

        await _sessionManager.RevokeSession(sessionValue);

        Options
            .CookieManager
            .DeleteCookie(Context, Options.SessionName, new());
    }
}
