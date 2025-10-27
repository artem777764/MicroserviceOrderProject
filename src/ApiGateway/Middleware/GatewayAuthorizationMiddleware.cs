using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Middleware;

public class GatewayAuthorizationMiddleware
{
    private readonly RequestDelegate _nextRequest;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public GatewayAuthorizationMiddleware(RequestDelegate nextRequest, TokenValidationParameters tokenValidationParameters)
    {
        _nextRequest = nextRequest;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        RemoveHeaders(context);

        string? authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.Headers["Gateway-Auth-Valid"] = "false";
            context.Request.Headers["Gateway-Auth-Reason"] = "absent";
            await _nextRequest(context);
            return;
        }

        string token = authorizationHeader.Substring("Bearer ".Length).Trim();
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        try
        {
            ClaimsPrincipal claimsPrincipal = jwtHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            string? userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? jti = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            string[] roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
            string[] roleIds = claimsPrincipal.FindAll("role_id").Select(c => c.Value).ToArray();
            string? activeRoleId = claimsPrincipal.FindFirst("active_role")?.Value;
            string? activeRoleName = claimsPrincipal.FindFirst("active_role_name")?.Value;

            context.Request.Headers["Gateway-Auth-Valid"] = "true";
            if (!string.IsNullOrEmpty(userId)) context.Request.Headers["Gateway-User-Id"] = userId;
            if (!string.IsNullOrEmpty(jti)) context.Request.Headers["Gateway-Auth-Jti"] = jti;
            if (roles.Length > 0) context.Request.Headers["Gateway-User-Roles"] = string.Join(",", roles);
            if (roleIds.Length > 0) context.Request.Headers["Gateway-Role-Ids"] = string.Join(",", roleIds);
            if (!string.IsNullOrEmpty(activeRoleId)) context.Request.Headers["Gateway-Active-Role-Id"] = activeRoleId;
            if (!string.IsNullOrEmpty(activeRoleName)) context.Request.Headers["Gateway-Active-Role-Name"] = activeRoleName;
            context.Request.Headers["Gateway-Auth-Reason"] = "ok";
        }
        catch (Exception)
        {
            context.Request.Headers["Gateway-Auth-Valid"] = "false";
            context.Request.Headers["Gateway-Auth-Reason"] = "error";
        }
        
        await _nextRequest(context);
    }
    
    private void RemoveHeaders(HttpContext context)
    {
        context.Request.Headers.Remove("Gateway-Auth-Valid");
        context.Request.Headers.Remove("Gateway-User-Id");
        context.Request.Headers.Remove("Gateway-User-Roles");
        context.Request.Headers.Remove("Gateway-Auth-Reason");
        context.Request.Headers.Remove("Gateway-Active-Role-Id");
    }
}