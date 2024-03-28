using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events;

public class EventTypeTotals
{
    [JsonPropertyName("event")] public string EventType { get; set; } = string.Empty;
    [JsonPropertyName("count")] public int Count { get; set; }
}