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

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var sessionValue = Options.CookieManager.GetRequestCookie(Context, Options.SessionName);
        
        if (string.IsNullOrEmpty(sessionValue))
        {
            return AuthenticateResult.Fail($"There is no corresponding session value for the key {Options.SessionName}!");
        }

        var session = await _sessionManager.GetSessionByValue(sessionValue!);

        if (session is null)
        {
            return AuthenticateResult.Fail($"Could not find a session with value {sessionValue}!");
        }

        if (session!.IsExpired() || session.IsRevoked)
        {
            return AuthenticateResult.Fail("Session either revoked or expired!");
        }

        var userPrinciple = GetCurrentUserPrinciple(session.UserId);
        var authTicket = new AuthenticationTicket(userPrinciple, new(), SessionAuthenticationDefaults.AuthenticationScheme);

        return AuthenticateResult.Success(authTicket);
    }

    private ClaimsPrincipal GetCurrentUserPrinciple(Guid userId)
    {
        var claims = new List<Claim>()
        {
            new Claim("Id", userId.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, SessionAuthenticationDefaults.AuthenticationScheme);

        return new ClaimsPrincipal(claimsIdentity);
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
        var sessionValue = Options.CookieManager.GetRequestCookie(Context, Options.SessionName);

        if (string.IsNullOrEmpty(sessionValue))
        {
            throw new Exception($"There is no corresponding session value for the key {Options.SessionName}");
        }

        await _sessionManager.RevokeSession(sessionValue);

        Options
            .CookieManager
            .DeleteCookie(Context, Options.SessionName, new());
    }
}
