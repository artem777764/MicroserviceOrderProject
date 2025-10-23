using ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddApplicationServices();

app.ConfigureApplication();

app.Run();