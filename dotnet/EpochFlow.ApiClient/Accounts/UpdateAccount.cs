using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Accounts;

public class UpdateAccount
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    public static UpdateAccount Create(string name)
    {
        return new UpdateAccount
        {
            Name = name
        };
    }
}