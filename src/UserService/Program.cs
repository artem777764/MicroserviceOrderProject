using backend.Services;
using Backend.Models;
using Backend.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using UserService.DTOs;
using UserService.Models.Context;
using UserService.Repositories;
using UserService.Repositories.interfaces;
using UserService.Services;
using UserService.Services.Interfaces;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(
        Environment.GetEnvironmentVariable("ApplicationDatabaseConnection")!
    )
);

builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

PrepareDb.PrepareDatabase(app);

app.Run();