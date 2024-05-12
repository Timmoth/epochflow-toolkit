using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables;

public class EventCountQuery : TableSubQuery
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
    public override string Type => "event_count";
}