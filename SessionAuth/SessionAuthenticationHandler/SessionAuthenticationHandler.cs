using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SessionAuth.SessionManager;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SessionAuth.SessionAuthenticationHandler;

public class SessionAuthenticationHandler : SignInAuthenticationHandler<SessionAuthenticationOptions>
{
    private readonly ISessionManager _sessionManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public SessionAuthenticationHandler(
        IOptionsMonitor<SessionAuthenticationOptions> options,
        ILoggerFactory logger, UrlEncoder encoder,
        ISystemClock clock,
        ISessionManager sessionManager, 
        IHttpContextAccessor contextAccessor) : base(options, logger, encoder, clock)
    {
        _sessionManager = sessionManager;
        _contextAccessor = contextAccessor;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        var userId = user.FindFirstValue("Id");
        var expirationTime = DateTime.UtcNow + Options.ExpireTimeSpan;

        var session = await _sessionManager.CreateSession(Guid.Parse(userId), expirationTime);

        _contextAccessor.HttpContext!.Response.Cookies.Append(Options.SessionName, session.Value, new());
    }

    protected override async Task HandleSignOutAsync(AuthenticationProperties? properties)
    {
        var sessionExists = _contextAccessor.HttpContext!.Request.Cookies.TryGetValue(Options.SessionName, out var sessionValue);

        if (!sessionExists || sessionValue == null)
        {
            throw new Exception($"There is no corrosponding session value for the key {Options.SessionName}");
        }

        await _sessionManager.RevokeSession(sessionValue);

        _contextAccessor.HttpContext!.Response.Cookies.Delete(Options.SessionName);
    }
}
