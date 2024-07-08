using OAuth.Models;

namespace OAuth.Services;

public interface IGithubUserService
{
    public Task<GitHubUser> GetAuthenticatedUser();

    public Task<string> GetAuthenticatedUserRepositories();
}
