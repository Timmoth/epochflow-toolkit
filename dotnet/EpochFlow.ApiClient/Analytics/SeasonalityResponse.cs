using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Analytics;

public class SeasonalityResponse
{
    [JsonPropertyName("daily")] public List<double>? Daily { get; set; }
    [JsonPropertyName("weekly")] public List<double>? Weekly { get; set; }
    [JsonPropertyName("monthly")] public List<double>? Monthly { get; set; }
}