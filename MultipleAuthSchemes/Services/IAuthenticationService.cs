using MultipleAuthSchemes.Models;

namespace MultipleAuthSchemes.Services;

public interface IAuthenticationService
{
    Task AuthenticateRegularUser(SignInRequest request);
    Task AuthenticateSpecialUser(SignInRequest request);
    Task SignOutUser(string schemeName);
}