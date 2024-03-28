using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Refit;

namespace EpochFlow.ApiClient.Events.Sets;

public class CreateEventSet
{
    [JsonPropertyName("name")]
    [AliasAs("name")]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
}