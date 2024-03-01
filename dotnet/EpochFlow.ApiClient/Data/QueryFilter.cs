using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Data;

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