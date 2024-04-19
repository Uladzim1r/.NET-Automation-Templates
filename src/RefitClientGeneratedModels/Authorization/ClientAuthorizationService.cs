using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RefitClientGeneratedModels.Clients;
using RefitClientGeneratedModels.Extensions;
using RefitClientGeneratedModels.Json.AppSettings;

namespace RefitClientGeneratedModels.Authorization;

public class BearerAuthorizationService(
    IAuthorizationClient authorizationClient,
    IOptions<AuthSetting> authorizationSetting,
    IMemoryCache memoryCache) : IAuthorizationService
{
    //Only single thread can request access token at a time to avoid duplicate autorization requests
    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public async Task<(string RoleName, string? AccessToken)> GetBearerTokenAsync(string? roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return ("Unauthorized", null);
        }

        using (var _ = await _semaphoreSlim.CreateDisposableLockAsync())
        {
            return await GetCachedOrRequestAsync(roleName);
        }
    }

    private Task<(string Role, string AccessToken)> GetCachedOrRequestAsync(string roleName)
    {
        return memoryCache.GetOrCreateAsync<(string Role, string AccessToken)>(nameof(GetCachedOrRequestAsync) + roleName,
            async cacheEntry =>
            {
                var token = await GetTokenForRoleAsync(roleName);
                //cacheEntry.AbsoluteExpiration = accessToken.ExpiresOn;
                return (roleName, token);
            });
    }

    // ReSharper disable once UnusedParameter.Local
    private async Task<string> GetTokenForRoleAsync(string roleName) //Ignoring role for now
    {
        var request = new TokenRequest
        {
            Password = authorizationSetting.Value.Password,
            UsernameOrEmailAddress = authorizationSetting.Value.Username
        };
        var accessToken = await authorizationClient.AuthenticateAsync(request);

        return "";
    }
}
