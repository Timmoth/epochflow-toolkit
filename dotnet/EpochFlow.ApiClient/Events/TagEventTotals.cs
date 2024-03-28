using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events;

public class TagEventTotals
{
    [JsonPropertyName("source")] public string Source { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
}