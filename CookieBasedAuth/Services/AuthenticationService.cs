using CookieBasedAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace CookieBasedAuth.Services;

public class AuthenticationService : IAuthenticationService
{

    private List<User> UsersLists = new List<User>()
    {
        new User(){ Id = 1, UserName = "Ahmad", Email = "ahmad.mousa@gmail.com", Password = "123" },
        new User(){ Id = 2, UserName = "Maysam", Email = "maysam.mousa@gmail.com", Password = "123" },
        new User(){ Id = 3, UserName = "Fadi", Email = "fadi.mousa@gmail.com", Password = "123" }
    };

    private readonly IHttpContextAccessor _httpContext;

    public AuthenticationService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public async Task AuthenticateUser(SignInRequest request)
    {
        var user = UsersLists.FirstOrDefault(x => x.Email == request.Email) ?? throw new Exception("User not found!");

        if (user.Password != request.Password)
        {
            throw new Exception("Incorrect Password!");
        }

        await SignInUser(user);
    }

    private async Task SignInUser(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "agent"),
            new Claim("Country", "Palestine")
        };

        var authProperties = new AuthenticationProperties
        {
            // AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.
            // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.
            //IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. Required when setting the 
            // ExpireTimeSpan option of CookieAuthenticationOptions 
            // set with AddCookie. Also required when setting 
            // ExpiresUtc.
            // IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.
            // RedirectUri = "~/Account/Index"
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };


        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var userClaimsPrinciple = new ClaimsPrincipal(userIdentity);

        await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimsPrinciple, authProperties);
    }

    public async Task LogOut()
    {
        await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
