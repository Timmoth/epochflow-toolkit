using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Models;

public class NumericListResponse
{
    [JsonPropertyName("headers")] public List<string> Headers { get; set; }
    [JsonPropertyName("data")] public double?[][] Data { get; set; }
}