using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.TrainSeasonalityModel;

public class TrainSeasonalityModelPipelineConfig
{
    [JsonPropertyName("filter")] public SeasonalModelFilterType Filter { get; set; }
    [JsonPropertyName("aggregation")] public SeasonalModelAggregation Aggregation { get; set; }
}

public class TrainSeasonalityModelPipeline : MeasurementPipeline
{
    [JsonPropertyName("config")] public TrainSeasonalityModelPipelineConfig Config { get; set; }
}