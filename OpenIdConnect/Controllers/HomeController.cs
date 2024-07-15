using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace OpenIdConnect.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HomeController(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    [HttpGet("~/")]
    public string HomePage()
    {
        return "Welcome to OpenIdConnect Demo project!";
    }

    [HttpGet("login/github")]
    public async Task LoginByGithub()
    {
        var authProps = new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:7202/api/home/oidc-successful-login"
        };

        await _contextAccessor.HttpContext!.ChallengeAsync("oidc-github", authProps);
    }

    [HttpGet("login/google")]
    public async Task LoginByGoogle()
    {
        var authProps = new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:7202/api/home/oidc-successful-login"
        };

        await _contextAccessor.HttpContext!.ChallengeAsync("oidc-google", authProps);
    }

    [HttpGet("oidc-successful-login")]
    public async Task<object> SuccessLogin()
    {
        var dict = new Dictionary<string, List<string>>();

        var claims = _contextAccessor.HttpContext!.User.Claims.Select(x => $"{x.Type}: {x.Value}").ToList();

        var authResult = await _contextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var tokens = authResult.Properties!
            .GetTokens()
            .Select(t => $"{t.Name}: {t.Value}")
            .ToList();

        dict["claims"] = claims;
        dict["tokens"] = tokens;

        return new
        {
            AuthenticationScheme = authResult.Properties!.Items[".AuthScheme"],
            AuthenticationDetails = dict
        };
    }
}
