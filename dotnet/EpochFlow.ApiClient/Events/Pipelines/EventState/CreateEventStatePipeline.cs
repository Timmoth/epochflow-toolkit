using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Pipelines.EventState;

public class CreateEventStatePipeline
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonPropertyName("grouping")] public EventStateGrouping Grouping { get; set; }
}