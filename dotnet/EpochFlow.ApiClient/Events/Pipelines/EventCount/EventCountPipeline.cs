using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Measurements.Pipelines;

namespace EpochFlow.ApiClient.Events.Pipelines.EventCount;

public enum EventCountFilter
{
    Source, Event
}

public class EventCountPipelineConfig
{
    [JsonPropertyName("set_id")] public string SetId { get; set; }
    [JsonPropertyName("filter")] public EventCountFilter Filter { get; set; }
}

public class EventCountPipeline : MeasurementPipeline
{
    [JsonPropertyName("config")] public EventCountPipelineConfig Config { get; set; }
}