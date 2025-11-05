using System.Text.Json;
using backend.Services;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using UserService.Models;
using UserService.Models.Context;
using UserService.Repositories;
using UserService.Repositories.Interfaces;
using UserService.Services;
using UserService.Services.Interfaces;

namespace Backend.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("ApplicationDatabase")!;

        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(connectionString)
        );

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Defect Managment Project API",
                Version = "v1"
            });
        });

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<JwtCookieService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDataRepository, UserDataRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        services.AddScoped<IUserService, UserServiceImpl>();
        services.AddScoped<IUserDataService, UserDataService>();
        services.AddScoped<IUserRoleService, UserRoleService>();

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
            app.UseSwagger(); // http://localhost:5126/swagger/index.html
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