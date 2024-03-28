using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Webhooks;

public class UpdateWebhook
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    [AliasAs("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("headers")]
    [AliasAs("headers")]
    public Dictionary<string, string> Headers { get; set; } = new();

    [JsonPropertyName("enabled")]
    [AliasAs("enabled")]
    public bool Enabled { get; set; } = false;

    [JsonPropertyName("update_interval")]
    [AliasAs("update_interval")]
    public int UpdateInterval { get; set; } = 3600;
}