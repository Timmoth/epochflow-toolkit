using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;
using Refit;

namespace EpochFlow.ApiClient.Data;

public class GetDataRequest
{
    [JsonPropertyName("start")]
    [AliasAs("start")]
    [Range(0, long.MaxValue)]
    public long Start { get; init; }

    [JsonPropertyName("end")]
    [AliasAs("end")]
    [Range(0, long.MaxValue)]
    public long End { get; init; }

    [JsonPropertyName("tags")]
    [AliasAs("tags")]
    [MinLength(0)]
    [MaxLength(10)]
    public List<string>? Tags { get; init; }

    [JsonPropertyName("resolution")]
    [AliasAs("resolution")]
    public QueryResolution? Resolution { get; set; }

    [JsonPropertyName("aggregation")]
    [AliasAs("aggregation")]
    public QueryAggregation? Aggregation { get; set; }

    public static GetDataRequest Create(long start, long end, List<string>? tags = null,
        QueryResolution? resolution = null, QueryAggregation? queryAggregation = null)
    {
        return new GetDataRequest
        {
            Start = start,
            End = end,
            Tags = tags,
            Resolution = resolution,
            Aggregation = queryAggregation
        };
    }

    protected bool Equals(GetDataRequest other)
    {
        return Start == other.Start && End == other.End && Equals(Tags, other.Tags) && Resolution == other.Resolution;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((GetDataRequest)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, Tags, Resolution);
    }
}