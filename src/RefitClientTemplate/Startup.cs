using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefitClientTemplate.Json.AppSettings;

namespace RefitClientTemplate;

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

        services.AddOptions<EndpointSetting>().BindConfiguration(nameof(EndpointSetting));
    }
}
