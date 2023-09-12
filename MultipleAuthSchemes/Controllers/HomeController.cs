using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MultipleAuthSchemes.Models;
using MultipleAuthSchemes.Services;

namespace MultipleAuthSchemes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private static string RegularUserSchemeName = CookieAuthenticationDefaults.AuthenticationScheme;
    private static string SpecialUserSchemeName = "Cookies2";

    private readonly IAuthenticationService _authenticationService;
    
    public HomeController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login-regular")]
    public async Task LogInRegularAsync(SignInRequest request)
    {
        await _authenticationService.AuthenticateRegularUser(request);
    }

    [HttpPost("login-special")]
    public async Task LogInSpecial(SignInRequest request)
    {
        await _authenticationService.AuthenticateSpecialUser(request);
    }

    [HttpPost("logout-regular")]
    public async Task LogOutRegularAsync()
    {
        await _authenticationService.SignOutUser(RegularUserSchemeName);
    }

    [HttpPost("logout-special")]
    public async Task LogOutSpecial(SignInRequest request)
    {
        await _authenticationService.SignOutUser(SpecialUserSchemeName);
    }
}
