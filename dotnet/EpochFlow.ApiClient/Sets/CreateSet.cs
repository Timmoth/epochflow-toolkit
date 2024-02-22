using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;
using Refit;

namespace EpochFlow.ApiClient.Sets
{
    public class CreateSet
    {
        [JsonPropertyName("name")]
        [AliasAs("name")]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("sample_mode")]
        [AliasAs("sample_mode")]
        public CollisionMode SampleMode { get; set; }

        public static CreateSet Create(string name,
            CollisionMode sampleMode = CollisionMode.Combine)
        {
            return new CreateSet
            {
                Name = name,
                SampleMode = sampleMode
            };
        }
    }
}