using Backend.Models;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using UserService.Models.Context;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(
        Environment.GetEnvironmentVariable("ApplicationDatabaseConnection")!
    )
);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

PrepareDb.PrepareDatabase(app);

app.Run();