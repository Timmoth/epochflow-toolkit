using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class WebhookBodyBase
{
    [JsonPropertyName("type")] public string Type { get; set; }
    [JsonPropertyName("project_id")] public string ProjectId { get; set; }
    [JsonPropertyName("webhook_id")] public string WebhookId { get; set; }
    [JsonPropertyName("timestamp")] public long Timestamp { get; set; }
}