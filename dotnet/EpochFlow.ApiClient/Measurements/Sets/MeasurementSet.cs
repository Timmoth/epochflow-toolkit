using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Measurements.Sets;

public class MeasurementSet
{
    [JsonPropertyName("project_id")] public string ProjectId { get; set; } = string.Empty;
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("sample_period")] public int SamplePeriod { get; set; }

    [JsonPropertyName("archive_requested_at")]
    public long? ArchiveRequestedAt { get; set; }

    [JsonPropertyName("archive_updated_at")]
    public long? ArchiveUpdatedAt { get; set; }
}