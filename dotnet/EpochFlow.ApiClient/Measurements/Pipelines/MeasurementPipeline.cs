using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines;

public class MeasurementPipeline
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("set_id")] public string SetId { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
}