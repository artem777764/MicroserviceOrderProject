using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiGateway.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Middleware;

public class GatewayAuthorizationMiddleware
{
    private readonly RequestDelegate _nextRequest;
    private readonly JwtSettings _jwtSettings;

    public GatewayAuthorizationMiddleware(RequestDelegate nextRequest, JwtSettings jwtSettings)
    {
        _nextRequest = nextRequest;
        _jwtSettings = jwtSettings;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        RemoveHeaders(context);

        if (!context.Request.Cookies.TryGetValue(_jwtSettings.JwtCookieName, out string? token) || string.IsNullOrWhiteSpace(token))
        {
            context.Request.Headers["Gateway-Auth-Valid"] = "false";
            context.Request.Headers["Gateway-Auth-Reason"] = "absent";
        }

        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        try
        {
            ClaimsPrincipal claimsPrincipal = jwtHandler.ValidateToken(token, GetTokenValidationParameters(_jwtSettings), out var validatedToken);
            string? userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? jti = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            string[] roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
            string[] roleIds = claimsPrincipal.FindAll("role_id").Select(c => c.Value).ToArray();
            string? activeRoleId = claimsPrincipal.FindFirst("active_role")?.Value;
            string? activeRoleName = claimsPrincipal.FindFirst("active_role_name")?.Value;

            context.Request.Headers["Gateway-Auth-Valid"] = "true";
            context.Request.Headers["Gateway-Auth-Reason"] = "ok";
            if (!string.IsNullOrEmpty(userId)) context.Request.Headers["Gateway-User-Id"] = userId;
            if (!string.IsNullOrEmpty(jti)) context.Request.Headers["Gateway-Auth-Jti"] = jti;
            if (roleIds.Length > 0) context.Request.Headers["Gateway-User-Role-Ids"] = string.Join(",", roleIds);
            if (roles.Length > 0) context.Request.Headers["Gateway-User-Role-Names"] = string.Join(",", roles);
            if (!string.IsNullOrEmpty(activeRoleId)) context.Request.Headers["Gateway-Active-Role-Id"] = activeRoleId;
            if (!string.IsNullOrEmpty(activeRoleName)) context.Request.Headers["Gateway-Active-Role-Name"] = activeRoleName;
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
        context.Request.Headers.Remove("Gateway-Auth-Reason");
        context.Request.Headers.Remove("Gateway-User-Id");
        context.Request.Headers.Remove("Gateway-Auth-Jti");
        context.Request.Headers.Remove("Gateway-User-Role-Ids");
        context.Request.Headers.Remove("Gateway-User-Role-Names");
        context.Request.Headers.Remove("Gateway-Active-Role-Id");
        context.Request.Headers.Remove("Gateway-Active-Role-Name");
    }
    
    private static TokenValidationParameters GetTokenValidationParameters(JwtSettings jwtSettings)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
        return new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,

            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateLifetime = true,
        };
    }
}