using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements
{
    public class MeasurementTag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
