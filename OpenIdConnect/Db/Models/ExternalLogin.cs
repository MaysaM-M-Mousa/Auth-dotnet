namespace OpenIdConnect.Db.Models;

public class ExternalLogin
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    // this the id in the external service
    public string ProviderKey { get; set; } = null!;

    // this is auth scheme
    public string Provider { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
