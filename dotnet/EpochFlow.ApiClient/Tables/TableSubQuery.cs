using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EventCountQuery), "event_count")]
[JsonDerivedType(typeof(EventNumericQuery), "event_numeric")]
[JsonDerivedType(typeof(MeasurementQuery), "measurement")]
public abstract class TableSubQuery
{
    [JsonPropertyName("type")]
    public abstract string Type { get; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}