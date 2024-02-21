using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QueryAggregation
{
    Sum,
    Average,
    Min,
    Max,
    Count
}