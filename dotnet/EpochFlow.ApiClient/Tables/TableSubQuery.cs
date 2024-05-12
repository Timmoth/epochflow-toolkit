using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EventCountQuery), "event_count")]
[JsonDerivedType(typeof(EventNumericQuery), "event_numeric")]
[JsonDerivedType(typeof(MeasurementQuery), "measurement")]
[JsonDerivedType(typeof(MeasurementSeasonalityQuery), "measurement_seasonality")]
public abstract class TableSubQuery
{
    [JsonPropertyName("type")]
    public abstract string Type { get; }
}