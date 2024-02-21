using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Analytics;
using Refit;

namespace EpochFlow.ApiClient.Sets;

public class UpdateSet
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("analytics")]
    [AliasAs("analytics")]
    public AnalyticsConfig? AnalyticsConfig { get; set; }

    public static UpdateSet Create(string name, AnalyticsConfig? analyticsConfig = null)
    {
        return new UpdateSet
        {
            Name = name,
            AnalyticsConfig = analyticsConfig
        };
    }
}