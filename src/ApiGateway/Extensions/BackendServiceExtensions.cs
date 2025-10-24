using System.Text.Json;

namespace ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy().LoadFromConfig(configuration.GetSection("ReverseProxy"));
        services.AddOpenApi();
        return services;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {

        }

        app.MapReverseProxy();
        app.UseHttpsRedirection();
        return app;
    }
}