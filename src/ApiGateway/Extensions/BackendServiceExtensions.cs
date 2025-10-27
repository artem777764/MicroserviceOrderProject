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
        JwtSettings jwt = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        services.AddSingleton(jwt);
        services.AddReverseProxy().LoadFromConfig(configuration.GetSection("ReverseProxy"));

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

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
        app.UseRouting();
        app.MapControllers();
        return app;
    }
}