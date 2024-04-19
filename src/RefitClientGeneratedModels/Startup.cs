using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using RefitClientGeneratedModels.Clients;
using RefitClientGeneratedModels.Json.AppSettings;

namespace RefitClientGeneratedModels;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("appSettings.json");
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local";
        configurationBuilder.AddJsonFile($"appSettings.{environment}.json", optional: true);
        configurationBuilder.AddEnvironmentVariables();

        services.AddSingleton<IConfiguration>(configurationBuilder.Build());

        services.AddOptions<AuthSetting>().BindConfiguration(nameof(AuthSetting));
        services.AddOptions<EndpointSetting>().BindConfiguration(nameof(EndpointSetting));

        services.AddConfiguredRefitHttpClient<IQuestionApi>((provider, client) =>
        {
            var requiredService = provider.GetRequiredService<IOptions<EndpointSetting>>().Value;
            client.BaseAddress = new Uri(requiredService.Url);
        });

        services.AddRefitClient<IAuthorizationClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var requiredService = provider.GetRequiredService<IOptions<AuthSetting>>().Value;
                client.BaseAddress = new Uri(requiredService.Url);
            });

        services.AddConfiguredRefitHttpClient<ITenantApi>((provider, client) =>
        {
            var requiredService = provider.GetRequiredService<IOptions<EndpointSetting>>().Value;
            client.BaseAddress = new Uri(requiredService.Url);
        });
    }
}
