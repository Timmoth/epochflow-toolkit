using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Measurements.Pipelines;

namespace EpochFlow.ApiClient.Events.Pipelines.EventState;

public class EventStatePipelineConfig
{
    [JsonPropertyName("grouping")] public EventStateGrouping Grouping { get; set; }
}

public class EventStatePipeline : MeasurementPipeline
{
    [JsonPropertyName("config")] public EventStatePipelineConfig Config { get; set; }
}