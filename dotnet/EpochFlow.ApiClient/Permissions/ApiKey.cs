using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Permissions;

public class ApiKey
{
    [JsonPropertyName("key_id")] public string KeyId { get; set; } = string.Empty;
    [JsonPropertyName("account_id")] public string AccountId { get; set; } = string.Empty;
    [JsonPropertyName("updated_at")] public long UpdatedAt { get; set; }
    [JsonPropertyName("created_at")] public long CreatedAt { get; set; }
    [JsonPropertyName("expires_at")] public long? ExpiresAt { get; set; }
    [JsonPropertyName("enabled")] public bool Enabled { get; set; }
    [JsonPropertyName("apikey_tail")] public string ApiKeyTail { get; set; } = string.Empty;
    [JsonPropertyName("key_name")] public string KeyName { get; set; } = string.Empty;
    [JsonPropertyName("is_admin")] public bool IsAdmin { get; set; } = false;

    [JsonPropertyName("all_set_operations")]
    public AllowedOperations AllowedOperations { get; set; }

    [JsonPropertyName("permissions")] public List<ApiKeyPermission> Permissions { get; set; } = new();
}