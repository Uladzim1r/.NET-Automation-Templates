using Refit;

namespace RefitClientGeneratedModels.Authorization;

public static class Headers
{
    public const string ActAs = "X-Act-As";
    public const string OverrideActAs = "X-Override-Act-As";
    public const string RoleName = "X-Role-Name";
}

[AttributeUsage(AttributeTargets.Parameter)]
public class ActAsIfDefinedAttribute() : HeaderAttribute(Headers.OverrideActAs);

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public class ActAsAttribute(string role) : HeadersAttribute($"{Authorization.Headers.ActAs}:{role}");
