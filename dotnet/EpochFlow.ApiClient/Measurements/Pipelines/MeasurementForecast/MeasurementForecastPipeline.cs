using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.MeasurementForecast;

public class MeasurementForecastPipelineConfig
{
    [JsonPropertyName("filter")] public MeasurementForecastFilterType Filter { get; set; }
}

public class MeasurementForecastPipeline : MeasurementPipeline
{
    [JsonPropertyName("config")] public MeasurementForecastPipelineConfig Config { get; set; }
}