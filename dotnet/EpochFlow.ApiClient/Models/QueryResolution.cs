using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QueryResolution
{
    Minute,
    Hour,
    Day,
    Week,
    Month
}