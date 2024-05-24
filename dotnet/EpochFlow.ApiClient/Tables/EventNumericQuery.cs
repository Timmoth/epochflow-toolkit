using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class EventNumericQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")]
    public List<string> Sources { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("events")]
    public List<string> Events { get; set; } = new();

    [JsonPropertyName("correlations")]
    public List<string> Correlations { get; set; } = new();

    [JsonPropertyName("properties")]
    public List<string> Properties { get; set; } = new();

    [JsonPropertyName("aggregation")] public List<QueryAggregation> Aggregation { get; set; } = new();

    public override string Type => "event_numeric";
}