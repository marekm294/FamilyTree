using NetTopologySuite.Geometries;

namespace Shared.Types;

public sealed class Place
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public Point? Coordinates { get; set; }
}