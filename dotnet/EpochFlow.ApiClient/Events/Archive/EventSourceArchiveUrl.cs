using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Archive;

public class EventSourceArchiveUrl
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("year")] public int Year { get; set; }
    [JsonPropertyName("month")] public int Month { get; set; }
}