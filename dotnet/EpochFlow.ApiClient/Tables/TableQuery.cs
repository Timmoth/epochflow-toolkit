using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Models;

namespace EpochFlow.ApiClient.Tables;

public class TableQuery
{
    [JsonPropertyName("start")]
    public long Start { get; set; }

    [JsonPropertyName("end")]
    public long End { get; set; }

    [JsonPropertyName("resolution")]
    public QueryResolution Resolution { get; set; }

    [JsonPropertyName("queries")]
    public TableSubQuery[] Queries { get; set; }
}