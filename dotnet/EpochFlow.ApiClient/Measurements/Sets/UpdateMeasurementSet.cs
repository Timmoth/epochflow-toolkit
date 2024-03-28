using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Measurements.Sets;

public class UpdateMeasurementSet
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public static UpdateMeasurementSet Create(string name)
    {
        return new UpdateMeasurementSet
        {
            Name = name
        };
    }
}