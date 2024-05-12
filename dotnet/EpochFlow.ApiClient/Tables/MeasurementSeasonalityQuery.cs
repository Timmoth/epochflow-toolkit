using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class MeasurementSeasonalityQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")]
    public string[]? Sources { get; set; }

    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

    [JsonPropertyName("aggregation")]
    public QueryAggregation Aggregation { get; set; }

    [JsonPropertyName("periods")]
    public int[] Periods { get; set; }
    public override string Type => "measurement_seasonality";
}