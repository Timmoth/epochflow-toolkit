using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class EventNumericQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")]
    public string[]? Sources { get; set; }

    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

    [JsonPropertyName("events")]
    public string[]? Events { get; set; }

    [JsonPropertyName("correlations")]
    public string[]? Correlations { get; set; }

    [JsonPropertyName("properties")]
    public string[] Properties { get; set; }

    [JsonPropertyName("aggregation")]
    public QueryAggregation[] Aggregation { get; set; }

    public override string Type => "event_numeric";
}