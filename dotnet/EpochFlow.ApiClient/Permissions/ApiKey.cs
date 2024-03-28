using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Permissions;

public enum ProjectRole
{
    None = 0,
    Member = 10,
    Admin = 20,
    Owner = 30
}

[Flags]
public enum SetOperations
{
    none = 0,
    read_data = 1,
    write_data = 2,
    access = 4,
    manage = 8
}

public class ApiKey
{
    [JsonPropertyName("project_id")] public string ProjectId { get; set; } = string.Empty;

    [JsonPropertyName("project_role")] public ProjectRole ProjectRole { get; set; }

    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }

    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }

    [JsonPropertyName("expires_at")] public long? ExpiresAt { get; set; }

    [JsonPropertyName("apikey_tail")] public string ApiKeyTail { get; set; } = string.Empty;
    [JsonPropertyName("enabled")] public bool Enabled { get; set; } = true;
    [JsonPropertyName("key_id")] public string KeyId { get; set; } = string.Empty;
    [JsonPropertyName("key_name")] public string KeyName { get; set; } = string.Empty;

    [JsonPropertyName("measurement_permissions")]
    public List<MeasurementPermission> MeasurementPermissions { get; set; } = new();

    [JsonPropertyName("event_permissions")]
    public List<EventPermission> EventPermissions { get; set; } = new();
}

public class MeasurementPermission
{
    [JsonPropertyName("set_id")] public string SetId { get; set; } = default!;

    [JsonPropertyName("sources")] public string[] Sources { get; set; }

    [JsonPropertyName("tags")] public string[] Tags { get; set; }
    [JsonPropertyName("operations")] public SetOperations Operations { get; set; }
}

public class EventPermission
{
    [JsonPropertyName("set_id")] public string SetId { get; set; } = default!;

    [JsonPropertyName("sources")] public string[] Sources { get; set; }

    [JsonPropertyName("tags")] public string[] Tags { get; set; }

    [JsonPropertyName("types")] public string[] Types { get; set; }

    [JsonPropertyName("operations")] public SetOperations Operations { get; set; }
}