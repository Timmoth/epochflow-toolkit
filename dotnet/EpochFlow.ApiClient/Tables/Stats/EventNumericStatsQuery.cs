using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables.Stats;

public class EventNumericStatsQuery : StatsTableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")] public List<string> Sources { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("events")]
    public List<string> Events { get; set; } = new();

    [JsonPropertyName("correlations")]
    public List<string> Correlations { get; set; }

    [JsonPropertyName("property")]
    public string Property { get; set; }

    public override string Type => "event_numeric";
}