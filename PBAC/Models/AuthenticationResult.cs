using PBAC.Models.Db;

namespace PBAC.Models;

public class AuthenticationResult
{
    public bool IsAuthenticated { get; set; }

    public User? User { get; set; }

    public DateTime LastAuthenticated { get; set; }

    public string AccessToken { get; set; } = null!;
}
