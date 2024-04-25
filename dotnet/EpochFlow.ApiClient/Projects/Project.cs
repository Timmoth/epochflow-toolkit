using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Projects;

public class Project
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("retention_period")]
    public int? RetentionPeriod { get; set; }
}