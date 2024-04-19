using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Measurements;

public class Measurement
{
    public Measurement(long timestamp, double value, string source, string[] tags)
    {
        Timestamp = timestamp;
        Value = value;
        Source = source;
        Tags = tags;
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

    [JsonPropertyName("tags")]
    [AliasAs("tags")]
    public string[] Tags { get; set; }

    public static Measurement Create(long timeStamp, double value, string source, string[] tags)
    {
        return new Measurement(timeStamp, value, source, tags);
    }
}