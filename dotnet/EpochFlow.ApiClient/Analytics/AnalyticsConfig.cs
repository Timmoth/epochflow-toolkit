using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Analytics;

public class AnalyticsConfig
{
    [JsonPropertyName("rolling_average")]
    [Range(1, 168)]
    public int RollingAverage { get; set; } = 24;

    [JsonPropertyName("anomaly_threshold")]
    public int AnomalyThreshold { get; set; } = 6;

    [JsonPropertyName("seasonality")] public Seasonality Seasonality { get; set; } = Seasonality.None;
}