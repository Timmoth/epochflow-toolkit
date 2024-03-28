using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Measurements;

public class Measurement
{
    public Measurement(long timestamp, double value, string source)
    {
        Timestamp = timestamp;
        Value = value;
        Source = source;
    }

    [JsonPropertyName("timestamp")]
    [AliasAs("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("value")]
    [AliasAs("value")]
    public double Value { get; set; }

    [JsonPropertyName("source")]
    [AliasAs("source")]
    public string Source { get; set; }

    public static Measurement Create(long timeStamp, double value, string source)
    {
        return new Measurement(timeStamp, value, source);
    }
}