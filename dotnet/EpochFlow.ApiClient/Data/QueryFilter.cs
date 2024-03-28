using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Data;

public record QueryFilter
{
    [JsonPropertyName("operator")] public QueryFilterComparison Comparison { get; set; }

    [JsonPropertyName("prop")] public QueryFilterProperty Property { get; set; }

    [JsonPropertyName("value")] public double Value { get; set; }

    public string Encode()
    {
        return $"{Property}-{Comparison}-{Value}";
    }
}