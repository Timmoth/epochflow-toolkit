using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Analytics;

namespace EpochFlow.ApiClient.Data
{
    public class LatestDataPoints
    {
        [JsonPropertyName("tag")] public string Tag { get; set; } = string.Empty;

        [JsonPropertyName("events")] public DataPoint? DataPoint { get; set; }
    }
}