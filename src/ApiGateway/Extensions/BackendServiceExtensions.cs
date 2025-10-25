using System.Text;
using System.Text.Json;
using ApiGateway.Middleware;
using ApiGateway.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(GetTokenValidationParameters(configuration));
        services.AddReverseProxy().LoadFromConfig(configuration.GetSection("ReverseProxy"));
        services.AddOpenApi();
        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {

        }
        app.UseMiddleware<GatewayAuthorizationMiddleware>();
        app.MapReverseProxy();
        app.UseHttpsRedirection();
        return app;
    }
    
    private static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {
        JwtSettings jwt = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
        return new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,

            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,

            ValidateAudience = true,
            ValidAudience = jwt.Audience,

            ValidateLifetime = true,
        };
    }
}