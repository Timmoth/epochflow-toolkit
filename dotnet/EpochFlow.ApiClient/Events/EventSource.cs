using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Events;

public class EventSource
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("archive_requested_at")]
    public long? ArchiveRequestedAt { get; set; }

    [JsonPropertyName("archive_updated_at")]
    public long? ArchiveUpdatedAt { get; set; }
}