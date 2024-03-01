using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;
using Refit;

namespace EpochFlow.ApiClient.Data
{
    public enum QueryFilterComparison
    {
        gt, gte, lt, lte, e
    }
    public enum QueryFilterProperty
    {
        Value, HourOfDay, DayOfWeek, DayOfMonth, DayOfYear, Year, Month
    }

    public class QueryFilter
    {
        [JsonPropertyName("operator")]
        public QueryFilterComparison Comparison { get; set; }

        [JsonPropertyName("prop")]
        public QueryFilterProperty Property { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        protected bool Equals(QueryFilter other)
        {
            return Comparison == other.Comparison && Property == other.Property && Math.Abs(Value - other.Value) < 0.01;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((QueryFilter)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Property, Comparison, Math.Round(Value, 10));
        }
    }

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
        public List<QueryFilter> Filters { get; set; } = new List<QueryFilter>();

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
                Filters = filters
            };
        }

        protected bool Equals(GetDataRequest other)
        {
            return Start == other.Start && End == other.End && Equals(Tag, other.Tag) &&
                   Resolution == other.Resolution && Aggregation == other.Aggregation && Filters.SequenceEqual(other.Filters);
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
            foreach (var filter in Filters)
            {
                filtersHashCode = HashCode.Combine(filter.GetHashCode(), filtersHashCode);
            }
            return HashCode.Combine(Start, End, Tag, Resolution, Aggregation, filtersHashCode);
        }
    }
}