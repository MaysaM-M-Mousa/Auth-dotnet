using JwtAuth.Db;
using JwtAuth.Models;

namespace JwtAuth.Services;

public interface IAuthenticationService
{
    public Task<string> AuthenticateUser(UserSignInRequest user);
    public string GenerateJwtToken(User user);
}
