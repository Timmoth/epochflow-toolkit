using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Archive;

public class MeasurementSourceArchiveUrl
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("year")] public int Year { get; set; }
    [JsonPropertyName("month")] public int Month { get; set; }
}