using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class WebhookLogResponse
{
    [JsonPropertyName("timestamp")] public long Timestamp { get; set; }

    [JsonPropertyName("retries")] public int Retries { get; set; }

    [JsonPropertyName("success")] public bool Success { get; set; }
}