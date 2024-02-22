using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Permissions;

public class CreateApiKey
{
    [JsonPropertyName("expires_at")] public long? ExpiresAt { get; set; }
    [JsonPropertyName("key_name")] public string KeyName { get; set; } = string.Empty;
    [JsonPropertyName("is_admin")] public bool IsAdmin { get; set; }
    [JsonPropertyName("all_set_operations")] public AllowedOperations AllSetOperations { get; set; }
    [JsonPropertyName("permissions")] public List<ApiKeyPermissionResponse> Permissions { get; set; } = new();
}