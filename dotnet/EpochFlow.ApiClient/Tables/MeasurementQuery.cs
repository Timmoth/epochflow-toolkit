using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class MeasurementQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")]
    public string[]? Sources { get; set; }

    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

    [JsonPropertyName("aggregation")]
    public QueryAggregation[] Aggregation { get; set; }
    public override string Type => "measurement";

    [JsonIgnore]
    public long[]? SourceIds { get; set; }

    [JsonIgnore]
    public long[]? TagIds { get; set; }
}