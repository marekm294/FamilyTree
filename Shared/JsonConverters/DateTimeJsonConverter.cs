using System.Text.Json.Serialization;
using System.Text.Json;

namespace Shared.JsonConverters;

public sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParse(reader.GetString(), out var date))
        {
            return date.ToLocalTime();
        }

        return default;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var utcValue = value.ToUniversalTime();
        writer.WriteStringValue(utcValue.ToString("O"));
    }
}