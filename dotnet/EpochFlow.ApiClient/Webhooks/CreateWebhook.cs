using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Webhooks;

public class CreateWebhook
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    public string Name { get; set; } = string.Empty;
}