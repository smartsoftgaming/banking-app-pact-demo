using Moq;
using BankingApp.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankingApp.Pact.Provider.Tests.ProviderState;

public static class PactProviderTestServices
{
    /// <summary>
    /// Registers everything the Pact provider test host needs: mocked repositories
    /// (both as <c>Mock&lt;T&gt;</c> and <c>T</c>), all <see cref="IProviderStateHandler"/>
    /// implementations from this assembly, and the dispatcher.
    /// </summary>
    public static IServiceCollection AddPactProviderTestDoubles(this IServiceCollection services)
    {
        AddMock<IAccountRepository>(services);
        AddMock<IUserRepository>(services);

        // Auto-discover every IProviderStateHandler in this test assembly.
        // Adding a new state = create one new handler class. No registration needed.

        var handlerType = typeof(IProviderStateHandler);
        var handlers = handlerType.Assembly.GetTypes()
            .Where(t => !t.IsAbstract && handlerType.IsAssignableFrom(t));

        foreach (var impl in handlers)
        {
            services.AddScoped(handlerType, impl);
        }

        services.AddScoped<TestDataSeeder>();
        return services;
    }

    private static void AddMock<T>(IServiceCollection services) where T : class
    {
        services.AddScoped(_ => new Mock<T>());
        services.AddScoped<T>(sp => sp.GetRequiredService<Mock<T>>().Object);
    }
}
