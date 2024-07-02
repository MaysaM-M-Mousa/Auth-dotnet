using SessionAuth.Models;

namespace SessionAuth.Services;

public interface IAuthenticationService
{
    public Task SignIn(SignInRequest request);
    public Task SingOut();
}
