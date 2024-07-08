using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace OAuth.Controllers;

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
        return "Welcome to OAuth Demo project!";
    }

    [HttpGet("login/github")]
    public async Task LoginByGithub()
    {
        var authProps = new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:7227/api/home/github-success"
        };

        await _contextAccessor.HttpContext!.ChallengeAsync("oauth", authProps);
    }

    [HttpGet("github-success")]
    public async Task<Dictionary<string, List<string>>> GithubSuccessLogin()
    {
        var dict = new Dictionary<string, List<string>>();

        var claims = _contextAccessor.HttpContext!.User.Claims.Select(x => $"{x.Type}:{x.Value}").ToList();

        var authResult = await _contextAccessor.HttpContext.AuthenticateAsync("oauth");

        var tokens = authResult.Properties!
            .GetTokens()
            .Select(t => $"{t.Name}:{t.Value}")
            .ToList();

        dict["claims"] = claims;
        dict["tokens"] = tokens;

        return dict;
    }
}
