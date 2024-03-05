using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Analytics;

public class DataPoint
{
    [JsonPropertyName("ts")] public long Timestamp { get; set; }

    [JsonPropertyName("v")] public double Value { get; set; }
}