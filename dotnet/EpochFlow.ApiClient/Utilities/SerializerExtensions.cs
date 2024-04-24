using System.Text.Json;
using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Events.Pipelines.EventState;
using EpochFlow.ApiClient.Measurements.Pipelines.TrainSeasonalityModel;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Permissions;

namespace EpochFlow.ApiClient.Utilities;

public static class SerializerExtensions
{
    public static JsonSerializerOptions Options = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals | JsonNumberHandling.AllowReadingFromString,
        Converters =
        {
            new EnumFlagsStringConverter<ProjectRole>(),
            new EnumFlagsStringConverter<SetOperations>(),
            new EnumFlagsStringConverter<SeasonalModelFilterType>(),
            new EnumFlagsStringConverter<SeasonalModelAggregation>(),
            new EnumFlagsStringConverter<EventStateGrouping>()
        }
    };

    public static JsonSerializerOptions ConfigureJsonSerializerOptions(this JsonSerializerOptions options)
    {
        options.WriteIndented = false;
        options.PropertyNameCaseInsensitive = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals |
                                 JsonNumberHandling.AllowReadingFromString;
        options.Converters.Add(new EnumFlagsStringConverter<ProjectRole>());
        options.Converters.Add(new EnumFlagsStringConverter<SetOperations>());
        options.Converters.Add(new EnumFlagsStringConverter<SeasonalModelFilterType>());
        options.Converters.Add(new EnumFlagsStringConverter<SeasonalModelAggregation>());
        options.Converters.Add(new EnumFlagsStringConverter<EventStateGrouping>());

        return options;
    }
}