using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements;

public class MeasurementStats
{
    [JsonPropertyName("min")]
    public double Min { get; set; }
    [JsonPropertyName("max")]
    public double Max { get; set; }
    [JsonPropertyName("average")]
    public double Average { get; set; }
    [JsonPropertyName("count")]

    public long Count { get; set; }

    [JsonPropertyName("percentile_01")]
    public double Percentile01 { get; set; }

    [JsonPropertyName("percentile_05")]
    public double Percentile05 { get; set; }

    [JsonPropertyName("percentile_25")]

    public double Percentile25 { get; set; }
    [JsonPropertyName("percentile_50")]

    public double Percentile50 { get; set; }
    [JsonPropertyName("percentile_75")]

    public double Percentile75 { get; set; }

    [JsonPropertyName("percentile_95")]
    public double Percentile95 { get; set; }

    [JsonPropertyName("percentile_99")]
    public double Percentile99 { get; set; }
    [JsonPropertyName("stdev")]

    public double Stdev { get; set; }
    [JsonPropertyName("first_timestamp")]

    public long FirstTimestamp { get; set; }
    [JsonPropertyName("last_timestamp")]

    public long LastTimestamp { get; set; }
}