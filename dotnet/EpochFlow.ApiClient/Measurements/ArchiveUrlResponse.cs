using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements
{
    public class ArchiveUrlResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
