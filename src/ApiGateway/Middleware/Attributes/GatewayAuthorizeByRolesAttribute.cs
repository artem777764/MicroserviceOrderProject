using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GatewayAuthorizeByRolesAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<string> _requiredRoles;

    public GatewayAuthorizeByRolesAttribute(params List<string> roles)
    {
        _requiredRoles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        HttpContext httpContext = context.HttpContext;
        string? isValid = httpContext.Request.Headers["Gateway-Auth-Valid"].FirstOrDefault();

        if (isValid != "true")
        {
            context.Result = new JsonResult(new { error = "Unauthorized" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        List<string> userRoles = httpContext.Request.Headers["Gateway-User-Roles"].FirstOrDefault()?.Split(',').ToList() ?? new List<string>();
        bool hasRequiredRole = _requiredRoles.Any(requiredRole => userRoles.Contains(requiredRole));
        if (!hasRequiredRole)
        {
            context.Result = new JsonResult(new { error = "Forbidden", message = "Insufficient permissions" })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}