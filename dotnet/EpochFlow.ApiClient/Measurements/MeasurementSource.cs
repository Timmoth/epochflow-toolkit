using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements
{
    public class MeasurementSource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latest_timestamp")]
        public long LatestTimestamp { get; set; }

        [JsonPropertyName("latest_value")]
        public double LatestValue { get; set; }

        [JsonPropertyName("archive_requested_at")]
        public long? ArchiveRequestedAt { get; set; }

        [JsonPropertyName("archive_updated_at")]
        public long? ArchiveUpdatedAt { get; set; }
    }
}
