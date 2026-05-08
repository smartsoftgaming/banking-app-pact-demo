using BankingApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProviderCore();

var app = builder.Build();

app.MapControllers();

app.Run();

