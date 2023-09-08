using CookieBasedAuth.Models;
using CookieBasedAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace CookieBasedAuth.Controllers;

[Route("Home")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public HomeController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task LogIn(SignInRequest request)
    {
        await _authenticationService.AuthenticateUser(request);
    }

    [HttpPost("logout")]
    public async Task LogOut()
    {
        await _authenticationService.LogOut();
    }

}
