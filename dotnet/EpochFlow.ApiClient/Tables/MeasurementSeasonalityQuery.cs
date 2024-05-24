using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class MeasurementSeasonalityQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")] public List<string> Sources { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("aggregation")]
    public QueryAggregation Aggregation { get; set; }

    [JsonPropertyName("periods")]
    public List<int> Periods { get; set; } = new();
    public override string Type => "measurement_seasonality";
}