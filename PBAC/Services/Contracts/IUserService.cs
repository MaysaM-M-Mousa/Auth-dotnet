using PBAC.Models;
using PBAC.Models.Db;

namespace PBAC.Services.Contracts;

public interface IUserService
{
    public Task RegisterUser(User user);

    public Task<AuthenticationResult> AuthenticateAsync(AuthenticationModel model);
}