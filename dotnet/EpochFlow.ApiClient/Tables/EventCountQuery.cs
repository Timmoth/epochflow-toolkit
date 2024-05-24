using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables;

public class EventCountQuery : TableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")]
    public List<string> Sources { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("events")]
    public List<string> Events { get; set; } = new();

    [JsonPropertyName("correlations")] public List<string> Correlations { get; set; } = new();
    public override string Type => "event_count";
}