using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Measurements;

public class GetLatestRequest
{
    [JsonPropertyName("sources")]
    [AliasAs("sources")]
    [MinLength(0)]
    [MaxLength(10)]
    public List<string>? Sources { get; set; }

    public static GetLatestRequest Create(List<string>? sources = null)
    {
        return new GetLatestRequest
        {
            Sources = sources
        };
    }
}