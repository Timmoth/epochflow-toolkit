using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Data
{
    public class GetTotalRequest
    {
        [JsonPropertyName("tags")]
        [AliasAs("tags")]
        [MinLength(0)]
        [MaxLength(10)]
        public List<string>? Tags { get; set; }

        public static GetTotalRequest Create(List<string>? tags = null)
        {
            return new GetTotalRequest
            {
                Tags = tags
            };
        }
    }
}