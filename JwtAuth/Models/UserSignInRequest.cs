namespace JwtAuth.Models;

public class UserSignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}