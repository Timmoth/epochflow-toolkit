using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class MeasurementQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")]
    public List<string> Sources { get; set; } = new List<string>();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new List<string>();

    [JsonPropertyName("aggregation")]
    public List<QueryAggregation> Aggregation { get; set; } = new();
    public override string Type => "measurement";

}