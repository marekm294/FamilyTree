namespace Services.Extensions;

public static class StringExtensions
{
    public static string ToBase64QuerySafe(this string base64string)
    {
        return base64string
            .Replace("+", "%");
    }

    public static string ToBase64QueryUnsafe(this string base64string)
    {
        return base64string
            .Replace("%", "+");
    }
}