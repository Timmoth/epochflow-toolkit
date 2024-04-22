using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Events;

public class EventDataPoint
{
    [JsonPropertyName("timestamp")]
    [AliasAs("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("source")]
    [AliasAs("source")]
    public string Source { get; set; }

    [JsonPropertyName("event")]
    [AliasAs("event")]
    public string Event { get; set; }

    [JsonPropertyName("correlation_id")]
    [AliasAs("correlation_id")]
    public string CorrelationId { get; set; }

    [JsonPropertyName("insert_id")]
    [AliasAs("insert_id")]
    public string? InsertId { get; set; }

    [JsonPropertyName("tags")]
    [AliasAs("tags")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [JsonPropertyName("string_state")]
    [AliasAs("string_state")]
    public Dictionary<string, string> StringState { get; set; } = new();

    [JsonPropertyName("numeric_state")]
    [AliasAs("numeric_state")]
    public Dictionary<string, double> NumericState { get; set; } = new();
}