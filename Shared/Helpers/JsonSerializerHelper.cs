using Shared.JsonConverters;
using System.Text.Json;

namespace Shared.Helpers;

public static class JsonSerializerHelper
{
    public static readonly JsonSerializerOptions CLIENT_JSON_SERIALIZER_OPTIONS = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = 
        {
            new DateTimeJsonConverter(),
            new PointJsonConverter(),
        },
    };
}