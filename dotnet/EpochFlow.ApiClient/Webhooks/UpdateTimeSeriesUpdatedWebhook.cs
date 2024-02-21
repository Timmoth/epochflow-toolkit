using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Webhooks;

public class UpdateTimeSeriesUpdatedWebhook
{
    [JsonPropertyName("url")]
    [AliasAs("url")]
    public string? Url { get; set; }

    [JsonPropertyName("headers")]
    [AliasAs("headers")]
    public Dictionary<string, string>? Headers { get; set; }

    [JsonPropertyName("enabled")]
    [AliasAs("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("update_interval")]
    [AliasAs("update_interval")]
    public int? UpdateInterval { get; set; }
}