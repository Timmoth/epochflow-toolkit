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
    public long Start { get; set; }

    [JsonPropertyName("end")]
    [AliasAs("end")]
    [Range(0, long.MaxValue)]
    public long End { get; set; }

    [JsonPropertyName("tag")]
    [AliasAs("tag")]
    public string Tag { get; set; } = string.Empty;

    [JsonPropertyName("resolution")]
    [AliasAs("resolution")]
    public QueryResolution Resolution { get; set; }

    [JsonPropertyName("aggregation")]
    [AliasAs("aggregation")]
    public QueryAggregation Aggregation { get; set; }

    [JsonPropertyName("filters")]
    [AliasAs("filters")]
    public List<string> Filters { get; set; } = new();

    public static GetDataRequest Create(long start, long end, string tag,
        QueryResolution resolution, QueryAggregation queryAggregation, List<QueryFilter> filters)
    {
        return new GetDataRequest
        {
            Start = start,
            End = end,
            Tag = tag,
            Resolution = resolution,
            Aggregation = queryAggregation,
            Filters = filters.Select(f => f.Encode()).ToList()
        };
    }

    protected bool Equals(GetDataRequest other)
    {
        return Start == other.Start && End == other.End && Equals(Tag, other.Tag) &&
               Resolution == other.Resolution && Aggregation == other.Aggregation &&
               Filters.SequenceEqual(other.Filters);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        return Equals((GetDataRequest)obj);
    }

    public override int GetHashCode()
    {
        var filtersHashCode = 0;
        foreach (var filter in Filters) filtersHashCode = HashCode.Combine(filter.GetHashCode(), filtersHashCode);

        return HashCode.Combine(Start, End, Tag, Resolution, Aggregation, filtersHashCode);
    }
}