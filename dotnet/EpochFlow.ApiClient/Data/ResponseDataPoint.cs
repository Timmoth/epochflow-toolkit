using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Data;

public class ResponseDataPoint
{
    [JsonPropertyName("tag")] public string Tag { get; set; } = string.Empty;

    [JsonPropertyName("events")] public List<double[]> Events { get; set; } = new();
}