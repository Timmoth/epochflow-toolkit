using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Webhooks;

namespace EpochFlow.ApiClient.Measurements.Pipelines.MeasurementUpdate;

public class LatestSourceMeasurementWebhookBody : WebhookBodyBase
{
    [JsonPropertyName("set_id")] public string SetId { get; set; }

    [JsonPropertyName("pipeline_id")] public string PipelineId { get; set; }
    // [JsonPropertyName("latest_measurements")] public List<LatestMeasurement> LatestMeasurements { get; set; }
}