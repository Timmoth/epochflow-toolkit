using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements;

public class MeasurementTotal
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }
    [JsonPropertyName("sum")] public double Sum { get; set; }
    [JsonPropertyName("average")] public double Average { get; set; }
    [JsonPropertyName("min")] public double Min { get; set; }
    [JsonPropertyName("max")] public double Max { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
}