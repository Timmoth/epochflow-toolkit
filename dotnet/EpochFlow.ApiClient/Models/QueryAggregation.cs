using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QueryAggregation
{
    Sum,
    Average,
    Min,
    Max,
    Count,
    P01,
    P05,
    P25,
    P50,
    P75,
    P95,
    P99,
    SD
}