namespace BankinhApp.Pact.Provider.Tests.ProviderState;

public interface IProviderStateHandler
{
    string State { get; }
    Task ApplyAsync();
}
