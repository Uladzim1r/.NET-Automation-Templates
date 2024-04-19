using RefitClientGeneratedModels.Authorization;
using Xunit.DependencyInjection;

namespace RefitClientGeneratedModels.HttpMessageHandlers;

public class TestOutputLoggingMessageHandler(ITestOutputHelperAccessor testOutputHelperAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.TryGetValues(Headers.RoleName, out var headerValues);
        var roleName = headerValues?.FirstOrDefault();

        var response = await base.SendAsync(request, cancellationToken);
        //Log response short information
        var message =
            $"[{request.Method}]\t" +
            $"[{roleName?[..5]}]\t" +
            $"[{(int)response.StatusCode}]\t" +
            $"{request.RequestUri!.PathAndQuery}";

        testOutputHelperAccessor.Output?.WriteLine(message);
        if (request.Method != HttpMethod.Get)
        {
            await LogHttpRequestAndResponseAsync(request, cancellationToken, response);
        }

        return response;
    }

    private async Task LogHttpRequestAndResponseAsync(HttpRequestMessage request, CancellationToken cancellationToken,
        HttpResponseMessage response)
    {
        if (request.Content != null)
        {
            var stringRequestContent = await request.Content.ReadAsStringAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(stringRequestContent))
            {
                testOutputHelperAccessor.Output?.WriteLine("Request content:");
                testOutputHelperAccessor.Output?.WriteLine(stringRequestContent);
            }
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        response.Content = new StringContent(responseContent);
        if (!string.IsNullOrWhiteSpace(responseContent))
        {
            testOutputHelperAccessor.Output?.WriteLine("Response content:");
            testOutputHelperAccessor.Output?.WriteLine(responseContent);
        }
    }
}
