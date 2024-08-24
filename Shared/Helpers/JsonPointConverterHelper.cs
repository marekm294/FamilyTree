using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Shared.Helpers;

internal class JsonPointConverterHelper
{
    public static Point? GetPointFromString(string? pointString)
    {
        if (string.IsNullOrWhiteSpace(pointString))
        {
            return null;
        }

        var wktReader = new WKTReader(NtsGeometryServices.Instance);
        var geometry = wktReader.Read(pointString);

        return geometry as Point;
    }

    public static string GetStringFromPoint(Point point)
    {
        return point.AsText();
    }
}
