using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Permissions;

[Flags]
public enum AllowedOperations
{
    Undefined = 0,
    Read = 1,
    Write = 2
}

public class ApiKeyPermissionResponse
{
    [JsonPropertyName("set_id")] public string SetId { get; set; } = string.Empty;

    [JsonPropertyName("tags")] public List<string> Tags { get; set; } = new();

    [JsonPropertyName("all_tags")] public bool AllTags { get; set; } = false;

    [JsonPropertyName("operations")] public AllowedOperations AllowedOperations { get; set; }
}