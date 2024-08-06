using NetTopologySuite.Geometries;
using Shared.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.JsonConverters;

public sealed class PointJsonConverter : JsonConverter<Point>
{
    public override Point? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return JsonPointConverterHelper.GetPointFromString(reader.GetString());
    }

    public override void Write(
        Utf8JsonWriter writer,
        Point value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(JsonPointConverterHelper.GetStringFromPoint(value));
    }
}