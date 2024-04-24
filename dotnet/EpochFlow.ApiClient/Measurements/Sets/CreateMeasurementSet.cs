using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;
using Refit;

namespace EpochFlow.ApiClient.Measurements.Sets;

public class CreateMeasurementSet
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("sample_period")]
    [AliasAs("sample_period")]
    public int SamplePeriod { get; set; }

    public static CreateMeasurementSet Create(string name, int samplePeriod)
    {
        return new CreateMeasurementSet
        {
            Name = name,
            SamplePeriod = samplePeriod
        };
    }
}