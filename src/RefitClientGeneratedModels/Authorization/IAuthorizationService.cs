namespace RefitClientGeneratedModels.Authorization;

public interface IAuthorizationService
{
    public Task<(string RoleName, string? AccessToken)> GetBearerTokenAsync(string? roleName);
}
