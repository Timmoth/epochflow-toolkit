using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables.Stats;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MeasurementStatsQuery), "measurement")]
[JsonDerivedType(typeof(EventNumericStatsQuery), "event_numeric")]
public abstract class StatsTableSubQuery
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public abstract string Type { get; }
}