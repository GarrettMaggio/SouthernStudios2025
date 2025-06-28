namespace SouthernStudios2025.Models;

public static class ParsingExtention
{
    public static int? SafeParseInt(this string x)
    {
        if (int.TryParse(x, out var result))
            return result;
        return null;
    }
}