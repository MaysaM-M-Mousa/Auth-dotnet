using JwtAuth.Db;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Extensions;

public static class DbExtention
{
    public static IServiceCollection AddJwtAuthDbConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JwtAuthDb>(opts => opts.UseSqlServer(configuration.GetConnectionString("JwtAuthDb")));

        return services;
    }
}
