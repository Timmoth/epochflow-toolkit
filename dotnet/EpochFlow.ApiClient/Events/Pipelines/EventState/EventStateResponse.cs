using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Pipelines.EventState;

public class EventStringStateValue
{
    [JsonPropertyName("value")] public string Value { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")]
    public long UpdatedAt { get; set; }
}

public class EventNumericStateValue
{
    [JsonPropertyName("value")] public double Value { get; set; }

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

    [JsonPropertyName("string_state")]
    public Dictionary<string, EventStringStateValue> StringState { get; set; } =
        new Dictionary<string, EventStringStateValue>();

    [JsonPropertyName("numeric_state")]
    public Dictionary<string, EventNumericStateValue> NumericState { get; set; } 
        = new Dictionary<string, EventNumericStateValue>();

}