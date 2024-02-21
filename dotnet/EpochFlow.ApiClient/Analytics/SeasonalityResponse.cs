using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Analytics;

public record SeasonalityResponse(
    [property: JsonPropertyName("daily")] List<double>? Daily,
    [property: JsonPropertyName("weekly")] List<double>? Weekly,
    [property: JsonPropertyName("monthly")]
    List<double>? Monthly,
    [property: JsonPropertyName("yearly")] List<double>? Yearly);