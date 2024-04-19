using System.Net.Http.Headers;
using RefitClientGeneratedModels.Authorization;

namespace RefitClientGeneratedModels.HttpMessageHandlers;

public class AuthorizationMessageHandler(IAuthorizationService authorizationService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.TryGetValues(Headers.ActAs, out var actAsHeaderValues);
        var actAsValue = actAsHeaderValues?.FirstOrDefault();
        request.Headers.TryGetValues(Headers.OverrideActAs, out var overriderActAsHeaderValue);
        var overrideActAsValue = overriderActAsHeaderValue?.FirstOrDefault();

        if (overrideActAsValue != null)
        {
            actAsValue = overrideActAsValue;

        }

        var accessToken = await authorizationService.GetBearerTokenAsync(actAsValue);

        //Add Header with RoleName to request for more explicit authorization
        request.Headers.Add(Headers.RoleName, accessToken.RoleName);

        if (accessToken.AccessToken != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
