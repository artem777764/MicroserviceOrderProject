using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GatewayAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        HttpContext httpContext = context.HttpContext;
        string? isValid = httpContext.Request.Headers["Gateway-Auth-Valid"].FirstOrDefault();
        string? reason = httpContext.Request.Headers["Gateway-Auth-Reason"].FirstOrDefault();

        if (isValid != "true")
        {
            context.Result = new JsonResult(new { error = "Unauthorized", reason = reason })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}