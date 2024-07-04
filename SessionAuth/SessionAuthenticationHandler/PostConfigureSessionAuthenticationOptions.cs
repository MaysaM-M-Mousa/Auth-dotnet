using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace SessionAuth.SessionAuthenticationHandler;

public class PostConfigureSessionAuthenticationOptions : IPostConfigureOptions<SessionAuthenticationOptions>
{
    public void PostConfigure(string name, SessionAuthenticationOptions options)
    {
        if (options.ExpireTimeSpan is null)
        {
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        }

        if (string.IsNullOrEmpty(options.SessionName))
        {
            options.SessionName = "session_id";
        }

        if (options.CookieManager is null)
        {
            options.CookieManager = new ChunkingCookieManager();
        }
    }
}
