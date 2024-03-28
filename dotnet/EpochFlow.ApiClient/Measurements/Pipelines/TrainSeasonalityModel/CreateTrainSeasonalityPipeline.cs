using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.TrainSeasonalityModel;

public enum SeasonalModelAggregation
{
    Average = 0,
    Sum = 1
}

public enum SeasonalModelFilterType
{
    None = 0,
    Source = 1,
    Tag = 2
}

public class CreateTrainSeasonalityPipeline
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonPropertyName("filter")] public SeasonalModelFilterType Filter { get; set; }

    [JsonPropertyName("aggregation")] public SeasonalModelAggregation Aggregation { get; set; }
}