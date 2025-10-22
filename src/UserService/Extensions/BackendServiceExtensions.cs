using System.Text.Json;
using backend.Services;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using UserService.Models.Context;
using UserService.Repositories;
using UserService.Repositories.interfaces;
using UserService.Services;
using UserService.Services.Interfaces;

namespace Backend.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(Environment.GetEnvironmentVariable("ApplicationDatabaseConnection")!
        ));

        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IEncryptionService, EncryptionService>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserService, UserServiceImpl>();

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
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        PrepareDb.PrepareDatabase(app);

        return app;
    }
}