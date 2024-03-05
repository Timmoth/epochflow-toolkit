using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class WebhookBody
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("timestamp")] public long Timestamp { get; set; }

    [JsonPropertyName("updates")]
    public List<TimeSeriesUpdatedWebhook> Updates { get; set; } = new();
}