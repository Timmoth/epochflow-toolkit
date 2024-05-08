using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.MeasurementForecast;

public enum MeasurementForecastFilterType
{
    None = 0,
    Source = 1,
    Tag = 2
}

public class CreateMeasurementForecastPipeline
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonPropertyName("filter")] public MeasurementForecastFilterType Filter { get; set; }
}