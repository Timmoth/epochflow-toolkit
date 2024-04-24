using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Utilities
{
    public class ListResponse<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }
    }
}
