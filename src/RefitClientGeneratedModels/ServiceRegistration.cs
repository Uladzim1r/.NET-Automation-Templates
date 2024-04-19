using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Refit;
using RefitClientGeneratedModels.Authorization;
using RefitClientGeneratedModels.HttpMessageHandlers;

namespace RefitClientGeneratedModels;

public static class ServiceRegistration
{
    public static IServiceCollection AddConfiguredRefitHttpClient<T>(this IServiceCollection services,
        Action<IServiceProvider, HttpClient> configureClient) where T : class
    {
        services.AddMemoryCache();

        services.TryAddTransient<TestOutputLoggingMessageHandler>();
        services.TryAddTransient<AuthorizationMessageHandler>();
        services.TryAddTransient<IAuthorizationService, BearerAuthorizationService>();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
            {
                Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore,
            })
        };

        services.AddRefitClient<T>(refitSettings)
            .AddHttpMessageHandler<AuthorizationMessageHandler>()
            .AddHttpMessageHandler<TestOutputLoggingMessageHandler>()
            .ConfigureHttpClient(configureClient);

        return services;
    }
}
