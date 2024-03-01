using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;
using Refit;

namespace EpochFlow.ApiClient.Analytics
{
    public class GetAnalytics
    {
        [JsonPropertyName("start")]
        [AliasAs("start")]
        [Range(0, long.MaxValue)]
        public long Start { get; set; }

        [JsonPropertyName("end")]
        [AliasAs("end")]
        [Range(0, long.MaxValue)]
        public long End { get; set; }

        [JsonPropertyName("tag")]
        [AliasAs("tag")]
        public string? Tag { get; set; }

        [JsonPropertyName("resolution")]
        [AliasAs("resolution")]
        public QueryResolution? Resolution { get; set; }

        public static GetAnalytics Create(long start, long end, string tag, QueryResolution? resolution = null)
        {
            return new GetAnalytics
            {
                Start = start,
                End = end,
                Tag = tag,
                Resolution = resolution,
            };
        }

        protected bool Equals(GetAnalytics other)
        {
            return Start == other.Start && End == other.End && Tag == other.Tag && Resolution == other.Resolution;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((GetAnalytics)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End, Tag, Resolution);
        }
    }
}