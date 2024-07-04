using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SessionAuth.SessionAuthenticationHandler;

public class SessionAuthenticationOptions : AuthenticationSchemeOptions
{ 
    public TimeSpan? ExpireTimeSpan { get; set; }

    public string SessionName { get; set; } = null!;

    public ICookieManager CookieManager { get; set; } = default!;
}
