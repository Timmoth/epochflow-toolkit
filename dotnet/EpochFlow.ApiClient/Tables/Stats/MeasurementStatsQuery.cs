using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables.Stats;

public class MeasurementStatsQuery : StatsTableSubQuery
{
    [JsonPropertyName("set_id")]
    public string SetId { get; set; }

    [JsonPropertyName("sources")] public List<string> Sources { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    public override string Type => "measurements";
}