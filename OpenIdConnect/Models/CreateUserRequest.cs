namespace OpenIdConnect.Models;

public class CreateUserRequest
{
    public string ProviderKey { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Provider { get; set; } = null!;
}
