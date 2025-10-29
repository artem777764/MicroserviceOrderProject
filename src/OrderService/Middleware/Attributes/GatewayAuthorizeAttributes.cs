using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderService.DTOs;
using OrderService.Models;

public class GatewayAuthorizeAttribute : Attribute, IAuthorizationFilter
{
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
    }
}