using CookieAuth.Models;

namespace CookieAuth.Services;

public interface IAuthenticationService
{
    public Task AuthenticateUser(SignInRequest request);
    public Task LogOut();
}
