using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.TrainSeasonalityModel;

public class UpdateEventCountPipeline
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }


    [JsonPropertyName("set_id")]
    [MinLength(3)]
    [MaxLength(255)]
    public string SetId { get; set; }
}