using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Data
{
    public class Measurement
    {
        public Measurement(long timestamp, double value, List<string> tags)
        {
            Timestamp = timestamp;
            Value = value;
            Tags = tags;
        }

        [JsonPropertyName("timestamp")]
        [AliasAs("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("value")]
        [AliasAs("value")]
        public double Value { get; set; }

        [JsonPropertyName("tags")]
        [AliasAs("tags")]
        public List<string> Tags { get; set; }

        public static Measurement Create(long timeStamp, double value, List<string> tags)
        {
            return new Measurement(timeStamp, value, tags);
        }
    }
}