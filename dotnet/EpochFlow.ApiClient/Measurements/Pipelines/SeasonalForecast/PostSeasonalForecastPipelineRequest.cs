using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.SeasonalForecast;

public class PostSeasonalForecastPipelineRequest
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonPropertyName("train_seasonal_model_pipeline")]
    public string TrainSeasonalModelPipelineId { get; set; }
}