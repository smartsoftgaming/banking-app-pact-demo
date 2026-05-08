using PactNet;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace BankingApp.Pact.Provider.Tests;


[Collection(BankingProviderPactCollection.Name)]
public sealed class BankingProviderPactTests
{
    private const string PactBrokerUsername = "admin";
    private const string PactBrokerPassword = "admin";
    private const string PactBrokerUrl = "http://localhost:9292";
    private const string PactProviderName = "AccountProvider";
    private const string ProviderVersion = "dev";

    private readonly BankingProviderFixture _fixture;
    private readonly ITestOutputHelper _output;

    public BankingProviderPactTests(BankingProviderFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
    public void VerifyProvider_FromPactBroker()
    {
        using var verifier = CreateVerifier();

        verifier
            .WithHttpEndpoint(_fixture.ProviderUri)
            .WithPactBrokerSource( new Uri(PactBrokerUrl), options =>
            {
                options.BasicAuthentication(PactBrokerUsername, PactBrokerPassword);
                options.PublishResults(ProviderVersion);
            })
            .WithProviderStateUrl(new Uri(_fixture.ProviderUri, BankingProviderFixture.ProviderStatesPath))
            .Verify();
    }

    private PactVerifier CreateVerifier()
    {
        var config = new PactVerifierConfig
        {
            Outputters = new[] { new XUnitOutput(_output) },
            LogLevel = PactLogLevel.Debug
        };

        return new PactVerifier(PactProviderName, config);
    }
}
