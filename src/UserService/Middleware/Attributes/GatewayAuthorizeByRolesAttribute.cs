using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserService.DTOs;
using UserService.Models;

public class GatewayAuthorizeByRolesAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<string> _requiredRoles;

    public GatewayAuthorizeByRolesAttribute(params string[] roles)
    {
        _requiredRoles = roles.ToList();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        HttpContext httpContext = context.HttpContext;
        string? isValid = httpContext.Request.Headers["Gateway-Auth-Valid"].FirstOrDefault();

        if (isValid != "true")
        {
            ApiResponseNoDataDTOBuilder apiResponseNoDataDTOBuilder = new ApiResponseNoDataDTOBuilder();
            apiResponseNoDataDTOBuilder.SetError(ResponseErrors.Unauthorized());

            context.Result = new JsonResult(apiResponseNoDataDTOBuilder.Build())
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        List<string> userRoles = httpContext.Request.Headers["Gateway-Active-Role-Name"].FirstOrDefault()?.Split(',').ToList() ?? new List<string>();
        bool hasRequiredRole = _requiredRoles.Any(requiredRole => userRoles.Contains(requiredRole));
        if (!hasRequiredRole)
        {
            ApiResponseNoDataDTOBuilder apiResponseNoDataDTOBuilder = new ApiResponseNoDataDTOBuilder();
            apiResponseNoDataDTOBuilder.SetError(ResponseErrors.Forbidden());

            context.Result = new JsonResult(apiResponseNoDataDTOBuilder.Build())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            return;
        }
    }
}