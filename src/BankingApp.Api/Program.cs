using BankingApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAccountProviderCore();

var app = builder.Build();

app.MapControllers();

app.Run();

