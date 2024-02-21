using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Analytics;

public record AnalyticsData(
    [property: JsonPropertyName("analytics")]
    List<double[]> Analytics);