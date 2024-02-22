using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Analytics
{
    public class AnalyticsData
    {
        [JsonPropertyName("analytics")] public List<double[]> Analytics { get; set; } = new List<double[]>();
    }
}