using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Analytics;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Sets
{
    public class Set
    {
        [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("sample_mode")] public CollisionMode SampleMode { get; set; }
        [JsonPropertyName("analytics")] public AnalyticsConfig? AnalyticsConfig { get; set; } = null;
    }
}