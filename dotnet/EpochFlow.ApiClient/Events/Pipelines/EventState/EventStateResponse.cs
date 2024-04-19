using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Pipelines.EventState;

public class EventStateValue
{
    [JsonPropertyName("value")] public object Value { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")]
    public long UpdatedAt { get; set; }
}

public class EventState
{
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; }

    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("pipeline_id")]
    public string PipelineId { get; set; }

    [JsonPropertyName("grouping")]
    public EventStateGrouping Grouping { get; set; }

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("state")]
    public Dictionary<string, EventStateValue> State { get; set; }
}