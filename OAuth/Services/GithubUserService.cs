using Microsoft.AspNetCore.Authentication;
using OAuth.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OAuth.Services;

public class GithubUserService : IGithubUserService
{
    private const string GithubClientName = "Github";

    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public GithubUserService(
        IHttpClientFactory clientFactory, 
        IHttpContextAccessor contextAccessor)
    {
        _clientFactory = clientFactory;
        _contextAccessor = contextAccessor;
    }

    public async Task<GitHubUser> GetAuthenticatedUser()
    {
        var client = await GetGithubClient();

        // user endpoint: https://api.github.com/user
        var endpoint = $"user";

        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        
        return JsonSerializer.Deserialize<GitHubUser>(await response.Content.ReadAsStringAsync());
    }

    public async Task<List<Repository>> GetAuthenticatedUserRepositories()
    {
        var user = await GetAuthenticatedUser();

        // repo endpoint: https://api.github.com/users/{username}/repos
        var endpoint = $"users/{user.Login}/repos";

        var client = await GetGithubClient();
        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<List<Repository>>(await response.Content.ReadAsStringAsync());
    }

    private async Task<HttpClient> GetGithubClient()
    {
        var client = _clientFactory.CreateClient(GithubClientName);

        var accessToken = await _contextAccessor.HttpContext!.GetTokenAsync("access_token");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return client;
    }
}