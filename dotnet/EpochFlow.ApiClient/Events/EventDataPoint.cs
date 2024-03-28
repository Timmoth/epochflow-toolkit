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
    public long CorrelationId { get; set; }
}