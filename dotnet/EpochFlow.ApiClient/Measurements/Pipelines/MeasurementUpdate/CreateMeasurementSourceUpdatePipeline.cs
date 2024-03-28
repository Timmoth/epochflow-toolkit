﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Measurements.Pipelines.MeasurementUpdate;

public class CreateMeasurementSourceUpdatePipeline
{
    [JsonPropertyName("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }


    [JsonPropertyName("webhook_id")]
    [MinLength(3)]
    [MaxLength(255)]
    public string WebhookId { get; set; }
}