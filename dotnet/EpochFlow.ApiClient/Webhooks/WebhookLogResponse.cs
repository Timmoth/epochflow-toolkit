using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class WebhookLogResponse
{
    [JsonPropertyName("timestamp")] public long Timestamp { get; set; }

    [JsonPropertyName("attempts")] public int Attempts { get; set; }

    [JsonPropertyName("status_code")] public int StatusCode { get; set; }
}