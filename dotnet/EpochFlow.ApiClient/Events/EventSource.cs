using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events;

public class EventSource
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}