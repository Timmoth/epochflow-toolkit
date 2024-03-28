using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Analytics;

namespace EpochFlow.ApiClient.Measurements;

public class LatestDataPoints
{
    [JsonPropertyName("source")] public string Source { get; set; } = string.Empty;

    [JsonPropertyName("events")] public DataPoint? DataPoint { get; set; }
}