using BankingApp.Api.Extensions;
using BankingApp.Pact.Provider.Tests.ProviderState;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BankingApp.Pact.Provider.Tests;

public sealed class BankingProviderFixture : IAsyncLifetime
{
    private WebApplication _app;
    public const string ProviderStatesPath = "/provider-states";

    public Uri ProviderUri { get; } = new("http://127.0.0.1:9222");

    public async Task InitializeAsync()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = "Testing"
        });

        builder.WebHost.UseKestrel().UseUrls(ProviderUri.ToString());

        // Same registrations Production uses (controllers, JSON, middleware, ...).
        builder.Services.AddProviderCore();

        // Mocked repositories + auto-discovered IProviderStateHandler implementations.
        builder.Services.AddPactProviderTestDoubles();

        _app = builder.Build();

        // Test-only endpoint. Not present in Program.cs, so it never ships to Production.
        _app.MapPost(ProviderStatesPath, async (ProviderStateRequest request, TestDataSeeder seeder) =>
        {
            await seeder.SetupAsync(request.State);
            return Results.Ok();
        });
        
        _app.MapControllers();

        await _app.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (_app is not null)
        {
            await _app.StopAsync();
            await _app.DisposeAsync();
        }
    }
}
