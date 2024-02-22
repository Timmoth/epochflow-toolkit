using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Webhooks
{
    public class CreateTimeSeriesUpdatedWebhook
    {
        [JsonPropertyName("url")]
        [AliasAs("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("headers")]
        [AliasAs("headers")]
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("enabled")]
        [AliasAs("enabled")]
        public bool Enabled { get; set; } = false;

        [JsonPropertyName("update_interval")]
        [AliasAs("update_interval")]
        public int UpdateInterval { get; set; }
    }
}