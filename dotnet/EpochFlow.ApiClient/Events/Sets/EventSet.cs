using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Sets;

public class EventSet
{
    [JsonPropertyName("project_id")] public string ProjectId { get; set; } = string.Empty;
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
}