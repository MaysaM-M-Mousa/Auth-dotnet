using AuthApp.Repositories;

namespace AuthApp.Extensions;

public static class RepositoriesExtentions
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
