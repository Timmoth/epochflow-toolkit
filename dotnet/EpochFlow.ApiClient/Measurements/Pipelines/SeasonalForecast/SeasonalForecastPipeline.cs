using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.SeasonalForecast;

public class SeasonalForecastPipelineConfig
{
    [JsonPropertyName("train_seasonal_model_pipeline")]
    public string TrainSeasonalModelPipelineId { get; set; } = string.Empty;

    [JsonPropertyName("set_id")] public string SetId { get; set; } = string.Empty;
}

public class SeasonalForecastPipeline : MeasurementPipeline
{
    [JsonPropertyName("config")] public SeasonalForecastPipelineConfig Config { get; set; }
}