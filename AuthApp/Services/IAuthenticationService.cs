using AuthApp.Db;
using AuthApp.Models;

namespace AuthApp.Services;

public interface IAuthenticationService
{
    public Task<string> AuthenticateUser(UserSignInRequest user);
    public string GenerateJwtToken(User user);
}
