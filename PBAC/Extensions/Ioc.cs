using PBAC.Services;
using PBAC.Services.Contracts;

namespace PBAC.Extensions;

public static class Ioc
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IUserService, UserService>();
    }
}
