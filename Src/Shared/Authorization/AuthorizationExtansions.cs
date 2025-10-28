namespace Anazon.Shared.Authorization;


public static class AuthorizationExtensions
{
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder, string permission) where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(op=> op.AddRequirements(new PermissionRequirement(permission)));
    }
}