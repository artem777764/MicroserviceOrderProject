using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.Models.Context;
using OrderService.Repositories;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;
using UserService.Repositories;

namespace OrderService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(Environment.GetEnvironmentVariable("ApplicationDatabaseConnection")!
        ));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Defect Managment Project API",
                Version = "v1"
            });
        });

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        
        services.AddScoped<IValidationService, ValidationService>();

        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IOrderService, OrderServiceImpl>();

        services.AddOpenApi();

        return services;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // http://localhost:5007/swagger/index.html
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Defect Managment Project API");
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();

        PrepareDb.PrepareDatabase(app);

        return app;
    }
}