using DotNetEnv;
using OrderService.Extensions;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApplication();

app.Run();