using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Accounts;

public record Account([property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name);