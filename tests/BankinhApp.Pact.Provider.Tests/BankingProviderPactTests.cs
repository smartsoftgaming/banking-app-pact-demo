using PactNet;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace BankinhApp.Pact.Provider.Tests;


[Collection(BankingProviderPactCollection.Name)]
public sealed class AccountProviderPactTests
{
    private const string PactBrokerUsername = "admin";
    private const string PactBrokerPassword = "admin";
    private const string ProviderStatesPath = "/provider-states";
    private const string ProviderName = "BankingProvider";
    private readonly BankingProviderFixture _fixture;
    private readonly ITestOutputHelper _output;
    private Uri BrokerUrl { get; } = new("http://localhost:9292");

    public AccountProviderPactTests(BankingProviderFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
    public void VerifyProvider_FromPactBroker()
    {
        using var verifier = CreateVerifier();

        verifier
            .WithHttpEndpoint(_fixture.ServerUri)
            .WithPactBrokerSource(BrokerUrl, options =>
            {
                options.BasicAuthentication(PactBrokerUsername, PactBrokerPassword);
                options.PublishResults("dev");
            })
            .WithProviderStateUrl(new Uri(_fixture.ServerUri, ProviderStatesPath))
            .Verify();
    }

    private PactVerifier CreateVerifier()
    {
        var config = new PactVerifierConfig
        {
            Outputters = new[] { new XUnitOutput(_output) },
            LogLevel = PactLogLevel.Debug
        };

        return new PactVerifier(ProviderName, config);
    }
}
