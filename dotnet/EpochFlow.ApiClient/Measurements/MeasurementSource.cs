using System.ComponentModel.DataAnnotations.Schema;
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

        [JsonPropertyName("earliest_timestamp")] public long EarliestTimestamp { get; set; }

        [JsonPropertyName("min")]
        public double Min { get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("p01")]
        public double P01 { get; set; }

        [JsonPropertyName("p05")]
        public double P05 { get; set; }

        [JsonPropertyName("p25")]
        public double P25 { get; set; }

        [JsonPropertyName("p50")]
        public double P50 { get; set; }

        [JsonPropertyName("p75")]
        public double P75 { get; set; }

        [JsonPropertyName("p95")]
        public double P95 { get; set; }

        [JsonPropertyName("p99")]
        public double P99 { get; set; }
    }
}
