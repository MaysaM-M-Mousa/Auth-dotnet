using Microsoft.AspNetCore.Authentication;
using SessionAuth.Models;
using SessionAuth.SessionAuthenticationHandler;
using System.Security.Claims;

namespace SessionAuth.Services;

public class AuthenticationService : IAuthenticationService
{
    
    private readonly IHttpContextAccessor _httpContext;
    private readonly IUserService _userService;

    public AuthenticationService(
        IHttpContextAccessor httpContext, 
        IUserService userService)
    {
        _httpContext = httpContext;
        _userService = userService;
    }

    public async Task SignIn(SignInRequest request)
    {
        var user = await _userService.GetUser(request.Email) ?? throw new Exception("Could not find user!");
        var userRoles = await _userService.GetUserRoles(user.Id);

        if (user.Password != request.Password)
        {
            throw new Exception("Incorrect Password!");
        }

        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString())
        };

        foreach(var role in userRoles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        var userIdentity = new ClaimsIdentity(claims, SessionAuthenticationDefaults.AuthenticationScheme);
        var userClaimsPrinciple = new ClaimsPrincipal(userIdentity);

        await _httpContext.HttpContext!.SignInAsync(SessionAuthenticationDefaults.AuthenticationScheme, userClaimsPrinciple);
    }
    
    public async Task SingOut()
    {
        await _httpContext.HttpContext!.SignOutAsync(SessionAuthenticationDefaults.AuthenticationScheme);
    }
}
