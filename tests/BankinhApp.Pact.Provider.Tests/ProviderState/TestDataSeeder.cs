using BankingApp.Core.Interfaces;
using Moq;

namespace BankinhApp.Pact.Provider.Tests.ProviderState;

/// <summary>
/// Resets all mocks before each provider state, then dispatches to the matching handler.
/// State logic itself lives in individual <see cref="IProviderStateHandler"/> classes.
/// </summary>
public sealed class TestDataSeeder
{
    private readonly Mock<IAccountRepository> _accountMock;
    private readonly Mock<IUserRepository> _userMock;
    private readonly Dictionary<string, IProviderStateHandler> _handlers;

    public TestDataSeeder(
        Mock<IAccountRepository> accountMock,
        Mock<IUserRepository> userMock,
        IEnumerable<IProviderStateHandler> handlers)
    {
        _accountMock = accountMock;
        _userMock = userMock;
        _handlers = handlers.ToDictionary(h => h.State, StringComparer.Ordinal);
    }

    public Task SetupAsync(string state)
    {
        _accountMock.Reset();
        _userMock.Reset();

        return _handlers.TryGetValue(state, out var handler)
            ? handler.ApplyAsync()
            : throw new InvalidOperationException($"Unknown provider state: '{state}'.");
    }
}
