using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Data
{
    public class GetLatestRequest
    {
        [JsonPropertyName("tags")]
        [AliasAs("tags")]
        [MinLength(0)]
        [MaxLength(10)]
        public List<string>? Tags { get; set; }

        public static GetLatestRequest Create(List<string>? tags = null)
        {
            return new GetLatestRequest
            {
                Tags = tags
            };
        }
    }
}