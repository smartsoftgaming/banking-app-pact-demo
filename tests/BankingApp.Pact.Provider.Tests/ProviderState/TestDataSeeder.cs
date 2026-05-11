using BankingApp.Core.Interfaces;
using Moq;

namespace BankingApp.Pact.Provider.Tests.ProviderState;

/// <summary>
/// Resets all mocks before each provider state, then dispatches to the matching handler.
/// State logic itself lives in individual <see cref="IProviderStateHandler"/> classes.
/// </summary>
public class TestDataSeeder(
    Mock<IAccountRepository> accountMock,
    Mock<IUserRepository> userMock,
    IEnumerable<IProviderStateHandler> handlers)
{
    private readonly Dictionary<string, IProviderStateHandler> _handlers = handlers.ToDictionary(h => h.State, StringComparer.Ordinal);

    public Task SetupAsync(string state)
    {
        accountMock.Reset();
        userMock.Reset();

        return _handlers.TryGetValue(state, out var handler)
            ? handler.ApplyAsync()
            : throw new InvalidOperationException($"Unknown provider state: '{state}'.");
    }
}