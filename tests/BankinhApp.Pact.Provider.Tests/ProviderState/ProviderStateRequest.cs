using System.Text.Json.Serialization;

namespace BankinhApp.Pact.Provider.Tests.ProviderState;

public class ProviderStateRequest
{
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
}
