using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MultipleAuthSchemes.Models;
using System.Security.Claims;

namespace MultipleAuthSchemes.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly List<User> _users = new List<User>()
    {
        new User(){ Id = 1, UserName = "Ahmad", Email = "ahmad.mousa@gmail.com", Password = "123" },
        new User(){ Id = 2, UserName = "Maysam", Email = "maysam.mousa@gmail.com", Password = "123" },
        new User(){ Id = 3, UserName = "Fadi", Email = "fadi.mousa@gmail.com", Password = "123" }
    };

    private readonly IHttpContextAccessor _contextAccessor;

    public AuthenticationService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task AuthenticateRegularUser(SignInRequest request)
    {
        var user = CheckValidLogin(request);

        var claims = new List<Claim>() {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, "regular")
        };

        var schemeName = CookieAuthenticationDefaults.AuthenticationScheme;

        var userIdentity = new ClaimsIdentity(claims, schemeName);

        var userClaimsPrinciple = new ClaimsPrincipal(userIdentity);

        await _contextAccessor.HttpContext.SignInAsync(schemeName, userClaimsPrinciple);
    }

    public async Task AuthenticateSpecialUser(SignInRequest request)
    {
        var user = CheckValidLogin(request);

        var claims = new List<Claim>() {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, "special")
        };

        var schemeName = "Cookies2";

        var userIdentity = new ClaimsIdentity(claims, schemeName);

        var userClaimsPrinciple = new ClaimsPrincipal(userIdentity);

        await _contextAccessor.HttpContext.SignInAsync(schemeName, userClaimsPrinciple);
    }

    private User CheckValidLogin(SignInRequest request)
    {
        var userDb = _users.FirstOrDefault(u => u.Email == request.Email) ?? throw new Exception("User Not Found!");

        if (userDb.Password != request.Password)
        {
            throw new Exception("Incorrect User Password!");
        }

        return userDb;
    }
    public async Task SignOutUser(string schemeName)
    {
        await _contextAccessor.HttpContext.SignOutAsync(schemeName);
    }
}
