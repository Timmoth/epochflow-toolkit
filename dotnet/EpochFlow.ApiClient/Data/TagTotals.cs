using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Data
{
    public class TagTotals
    {
        [JsonPropertyName("tag")] public string Tag { get; set; } = string.Empty;
        [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }
        [JsonPropertyName("sum")] public double Sum { get; set; }
        [JsonPropertyName("min")] public double Min { get; set; }
        [JsonPropertyName("max")] public double Max { get; set; }
        [JsonPropertyName("count")] public int Count { get; set; }
    }
}