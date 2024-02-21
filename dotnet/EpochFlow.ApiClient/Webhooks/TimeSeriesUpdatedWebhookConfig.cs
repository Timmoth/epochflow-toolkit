using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class TimeSeriesUpdatedWebhookConfig
{
    [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;

    [JsonPropertyName("headers")] public Dictionary<string, string> Headers { get; set; } = new();

    [JsonPropertyName("enabled")] public bool Enabled { get; set; } = false;

    [JsonPropertyName("recent_requests")] public List<WebhookLogResponse> RecentRequests { get; set; } = new();

    [JsonPropertyName("update_interval")] public int UpdateInterval { get; set; }

    [JsonPropertyName("update_at")] public long UpdateAt { get; set; }
}