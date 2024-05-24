using System.Text.Json;
using System.Text.Json.Serialization;
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

        return options;
    }
}