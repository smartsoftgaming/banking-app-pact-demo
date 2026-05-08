namespace BankinhApp.Pact.Provider.Tests;

[CollectionDefinition(Name)]
public sealed class BankingProviderPactCollection : ICollectionFixture<BankingProviderFixture>
{
    public const string Name = "Banking Provider Pact Collection";
}
