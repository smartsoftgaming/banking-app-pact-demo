using BankingApp.Core.Interfaces;
using Moq;

namespace BankingApp.Pact.Provider.Tests.ProviderState;

/// <summary>
/// Dispatches to the matching handler for each provider state.
/// State logic itself lives in individual <see cref="IProviderStateHandler"/> classes.
/// </summary>
public class TestDataSeeder(IEnumerable<IProviderStateHandler> handlers)
{
    private readonly Dictionary<string, IProviderStateHandler> _handlers = handlers.ToDictionary(h => h.State, StringComparer.Ordinal);

    public Task SetupAsync(string state)
    {
        return _handlers.TryGetValue(state, out var handler)
            ? handler.ApplyAsync()
            : throw new InvalidOperationException($"Unknown provider state: '{state}'.");
    }
}
