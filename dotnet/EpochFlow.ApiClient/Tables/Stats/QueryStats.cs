using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Tables.Stats;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QueryStats
{
    Min,
    Max,
    Average,
    Sum,
    Count,
    P01,
    P05,
    P25,
    P50,
    P75,
    P95,
    P99,
}