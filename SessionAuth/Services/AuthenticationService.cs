using Microsoft.AspNetCore.Authentication;
using SessionAuth.Models;
using SessionAuth.SessionAuthenticationHandler;
using System.Security.Claims;

namespace SessionAuth.Services;

public class AuthenticationService : IAuthenticationService
{
    private List<User> UsersLists = new List<User>()
    {
        new User(){ Id = new Guid("ec4575be-a93c-4dc6-a7ab-d221461b37ea"), Name = "Ahmad", Email = "ahmad.mousa@gmail.com", Password = "123" },
        new User(){ Id = new Guid("5af8d290-a9af-4c35-80e0-ad316f5a1c3f"), Name = "Maysam", Email = "maysam.mousa@gmail.com", Password = "123" },
        new User(){ Id = new Guid("9fde31ed-08a6-4c97-9c92-99299a992673"), Name = "Fadi", Email = "fadi.mousa@gmail.com", Password = "123" }
    };

    private readonly IHttpContextAccessor _httpContext;

    public AuthenticationService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public async Task SignIn(SignInRequest request)
    {
        var user = UsersLists.FirstOrDefault(x => x.Email == request.Email) ?? throw new Exception("User not found!");

        if (user.Password != request.Password)
        {
            throw new Exception("Incorrect Password!");
        }

        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString())
        };

        var userIdentity = new ClaimsIdentity(claims, SessionAuthenticationDefaults.AuthenticationScheme);
        var userClaimsPrinciple = new ClaimsPrincipal(userIdentity);

        await _httpContext.HttpContext!.SignInAsync(SessionAuthenticationDefaults.AuthenticationScheme, userClaimsPrinciple);
    }
    
    public async Task SingOut()
    {
        await _httpContext.HttpContext!.SignOutAsync(SessionAuthenticationDefaults.AuthenticationScheme);
    }
}
