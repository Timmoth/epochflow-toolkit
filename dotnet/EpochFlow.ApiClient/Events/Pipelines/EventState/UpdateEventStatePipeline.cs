using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Pipelines.EventState;

public class UpdateEventStatePipeline
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }
}