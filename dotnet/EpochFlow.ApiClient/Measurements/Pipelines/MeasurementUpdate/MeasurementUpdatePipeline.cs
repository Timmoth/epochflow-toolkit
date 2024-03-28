using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.MeasurementUpdate;

public class MeasurementUpdatePipelineConfig
{
    [JsonPropertyName("webhook_id")] public string WebhookId { get; set; } = string.Empty;
}

public class MeasurementUpdatePipeline : MeasurementPipeline
{
    [JsonPropertyName("config")] public MeasurementUpdatePipelineConfig Config { get; set; }
}