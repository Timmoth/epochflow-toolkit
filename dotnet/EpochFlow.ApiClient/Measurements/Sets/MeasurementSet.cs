using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Measurements.Sets;

public class MeasurementSet
{
    [JsonPropertyName("project_id")] public string ProjectId { get; set; } = string.Empty;
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("sample_period")] public SamplePeriod SamplePeriod { get; set; }
}