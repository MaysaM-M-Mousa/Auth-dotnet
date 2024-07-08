namespace OAuth.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddNamedHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient("Github", client =>
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        });

        return services;
    }
}
