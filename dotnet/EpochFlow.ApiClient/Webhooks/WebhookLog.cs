using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class WebhookLog
{
    [JsonPropertyName("pipeline_type")] public string PipelineType { get; set; }

    [JsonPropertyName("pipeline_id")] public string PipelineId { get; set; }

    [JsonPropertyName("timestamp")] public long Timestamp { get; set; }

    [JsonPropertyName("attempts")] public int Attempts { get; set; }

    [JsonPropertyName("status_code")] public int StatusCode { get; set; }
}