

namespace Anazon.Utils;


public static class StringUtils
{
    public static string AsNormalized(this string input)
    {
        return input.Trim().ToLowerInvariant();
    }
}