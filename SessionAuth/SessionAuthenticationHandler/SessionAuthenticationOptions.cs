using Microsoft.AspNetCore.Authentication;

namespace SessionAuth.SessionAuthenticationHandler;

public class SessionAuthenticationOptions : AuthenticationSchemeOptions
{ 
    public TimeSpan ExpireTimeSpan { get; set; } = TimeSpan.FromHours(1);

    public string SessionName { get; set; } = "session_id";
}
