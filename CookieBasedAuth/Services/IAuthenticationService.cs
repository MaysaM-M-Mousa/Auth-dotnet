using CookieBasedAuth.Models;

namespace CookieBasedAuth.Services;

public interface IAuthenticationService
{
    public Task AuthenticateUser(SignInRequest request);
    public Task LogOut();
}
