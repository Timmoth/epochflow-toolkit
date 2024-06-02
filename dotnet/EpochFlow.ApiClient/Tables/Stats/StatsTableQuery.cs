using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables.Stats;

public class StatsTableQuery
{
    [JsonPropertyName("start")]
    public long? Start { get; set; }

    [JsonPropertyName("end")]
    public long? End { get; set; }

    [JsonPropertyName("realtime")] 
    public bool? Realtime { get; set; } = false;

    [JsonPropertyName("stats")] public List<QueryStats> Stats { get; set; } = new();

    [JsonPropertyName("queries")]
    public List<StatsTableSubQuery> Queries { get; set; } = new();
}