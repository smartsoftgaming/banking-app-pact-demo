using Moq;
using BankingApp.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace BankingApp.Pact.Provider.Tests.ProviderState;

public static class PactProviderTestServices
{
    /// <summary>
    /// Registers the dependencies used by Pact provider tests,
    /// including repository mocks and provider state handlers.
    /// </summary>

    public static IServiceCollection AddPactProviderTestDoubles(this IServiceCollection services)
    {
        AddMock<IAccountRepository>(services);
        AddMock<IUserRepository>(services);

        //services.AddSingleton<IProviderStateHandler, AccountExistsForBalanceState>();
        //services.AddSingleton<IProviderStateHandler, AccountExistsForOverdraftState>();
        //services.AddSingleton<IProviderStateHandler, UserExistsState>();
        //services.AddSingleton<IProviderStateHandler, AccountNotFoundState>();

        // Auto-discover every IProviderStateHandler in this test assembly.
        // Adding a new state = create one new handler class. No registration needed.

        var handlerType = typeof(IProviderStateHandler);
        var handlers = handlerType.Assembly.GetTypes()
            .Where(t => !t.IsAbstract && handlerType.IsAssignableFrom(t));

        foreach (var impl in handlers)
        {
            services.AddSingleton(handlerType, impl);
        }

        services.AddSingleton<TestDataSeeder>();
        return services;
    }

    private static void AddMock<T>(IServiceCollection services) where T : class
    {
        var mock = new Mock<T>();
        services.AddSingleton(mock);
        services.AddSingleton<T>(_ => mock.Object);
    }
}