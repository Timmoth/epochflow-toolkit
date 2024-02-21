using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Analytics;

public record DataPoint([property: JsonPropertyName("ts")] long Timestamp,
    [property: JsonPropertyName("v")] double Value);