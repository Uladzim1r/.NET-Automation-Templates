using Refit;

namespace RefitClientGeneratedModels.Clients;

[Headers("Content-Type", "application/json")]
public interface IAuthorizationClient
{
    [Post("/api/TokenAuth/Authenticate")]
    Task<Dictionary<string, object?>> AuthenticateAsync([Body] TokenRequest request);
}

public class TokenRequest
{
    public required string UsernameOrEmailAddress { get; set; }
    public required string Password { get; set; }
}
