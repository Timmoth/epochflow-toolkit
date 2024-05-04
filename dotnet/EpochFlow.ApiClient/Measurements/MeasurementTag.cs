using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements
{
    public class MeasurementTag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latest_timestamp")]
        public long LatestTimestamp { get; set; }

        [JsonPropertyName("latest_value")]
        public double LatestValue { get; set; }

        [JsonPropertyName("earliest_timestamp")] public long EarliestTimestamp { get; set; }

        [JsonPropertyName("min")]
        public double Min { get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }
    }
}
