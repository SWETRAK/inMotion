using IMS.Shared.Models.Exceptions;

namespace IMS.Shared.Utils.Parsers;

public static class ParseGuidFromStringUtil
{
    /// <summary>
    /// Parses guid from string
    /// </summary>
    /// <param name="value">String value of Guid in form 8-4-4-4-12</param>
    /// <returns>Guid from string</returns>
    /// <exception cref="InvalidGuidStringException">If Guid cant be parsed</exception>
    public static Guid ParseGuid(this string value)
    {
        if (!Guid.TryParse(value, out var result))
            throw new InvalidGuidStringException(value);
        return result;
    }
}