using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Permissions;

public class CreateApiKey
{
    [JsonPropertyName("expires_at")] public long? ExpiresAt { get; set; }
    [JsonPropertyName("key_name")] public string KeyName { get; set; } = string.Empty;

    [JsonPropertyName("project_role")] public ProjectRole ProjectRole { get; set; }

    [JsonPropertyName("measurement_permissions")]
    public List<MeasurementPermission> MeasurementPermissions { get; set; } = new();

    [JsonPropertyName("event_permissions")]
    public List<EventPermission> EventPermissions { get; set; } = new();
}