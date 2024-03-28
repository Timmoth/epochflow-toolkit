using System.Text.Json;
using System.Text.Json.Serialization;

namespace EpochFlow.ApiClient.Utilities;

public class EnumFlagsStringConverter<TEnum> : JsonConverter<TEnum> where TEnum : Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected a string for enum flags parsing, but got {reader.TokenType}.");
        }

        var value = reader.GetString();
        var flagValues = value!.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        TEnum result = default!;

        foreach (var flagValue in flagValues)
        {
            if (!Enum.TryParse(typeof(TEnum), flagValue, true, out var flag))
            {
                throw new JsonException($"Unable to parse enum flag value '{flagValue}'.");
            }

            result = (TEnum)(object)((int)(object)result | (int)flag);
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}