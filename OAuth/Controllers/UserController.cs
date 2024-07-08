using Microsoft.AspNetCore.Mvc;
using OAuth.Models;
using OAuth.Services;

namespace OAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IGithubUserService _githubUserService;

    public UserController(IGithubUserService githubUserService)
    {
        _githubUserService = githubUserService;
    }

    [HttpGet("github/me")]
    public async Task<GitHubUser> GetGithubAuthDetails()
    {
        return await _githubUserService.GetAuthenticatedUser();
    }

    [HttpGet("github/repositories/me")]
    public async Task<string> GetGithubRepositories()
    {
        return await _githubUserService.GetAuthenticatedUserRepositories();
    }
}
