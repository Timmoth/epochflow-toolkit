using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events.Archive;

public class EventSourceArchive
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("year")] public int Year { get; set; }
    [JsonPropertyName("month")] public int Month { get; set; }

    [JsonPropertyName("updated_at")]
    public long? UpdatedAt { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

}