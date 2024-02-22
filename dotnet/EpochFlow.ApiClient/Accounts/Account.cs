using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Accounts
{
    public class Account
    {
        [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    }
}