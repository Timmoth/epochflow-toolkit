﻿using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Webhooks;

public class MeasurementsUpdatedWebhook
{
    [JsonPropertyName("set_id")] public string SetId { get; set; }
    [JsonPropertyName("source")] public string Source { get; set; }
    [JsonPropertyName("start_timestamp")] public long Start { get; set; }
    [JsonPropertyName("end_timestamp")] public long End { get; set; }
    [JsonPropertyName("latest_value")] public double LatestValue { get; set; }
}