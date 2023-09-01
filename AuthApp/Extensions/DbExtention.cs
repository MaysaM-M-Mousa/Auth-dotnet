using AuthApp.Db;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Extensions;

public static class DbExtention
{
    public static IServiceCollection AddAuthDbConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthAppDb>(opts => opts.UseSqlServer(configuration.GetConnectionString("AuthAppDb")));

        return services;
    }
}
