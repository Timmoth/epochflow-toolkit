using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Measurements;

public class GetTotalRequest
{
    [JsonPropertyName("sources")]
    [AliasAs("sources")]
    [MinLength(0)]
    [MaxLength(10)]
    public List<string>? Sources { get; set; }

    public static GetTotalRequest Create(List<string>? sources = null)
    {
        return new GetTotalRequest
        {
            Sources = sources
        };
    }
}