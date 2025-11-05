using DotNetEnv;
using OrderService.Extensions;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"-> {builder.Configuration.GetConnectionString("ApplicationDatabase")}");

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApplication();

app.Run();