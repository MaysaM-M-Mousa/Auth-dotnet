using Microsoft.AspNetCore.Mvc;
using PBAC.Models;
using PBAC.Models.Db;
using PBAC.Services.Contracts;

namespace PBAC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("sign-up")]
    public async Task RegisterUser(User user)
    {
        await _userService.RegisterUser(user);
    }

    [HttpPost("login")]
    public async Task<AuthenticationResult> LogIn(AuthenticationModel model)
    {
        return await _userService.AuthenticateAsync(model);
    }
}
