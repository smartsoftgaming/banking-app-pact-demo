using System.Text.Json.Serialization;

namespace BankingApp.Pact.Provider.Tests.ProviderState;

public class ProviderStateRequest
{
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
}
