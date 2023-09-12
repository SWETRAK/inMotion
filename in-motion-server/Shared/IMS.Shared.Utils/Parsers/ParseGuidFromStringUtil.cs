using IMS.Shared.Models.Exceptions;

namespace IMS.Shared.Utils.Parsers;

public static class ParseGuidFromStringUtil
{
    public static Guid ParseGuid(this string value)
    {
        if (!Guid.TryParse(value, out var result))
            throw new InvalidGuidStringException(value);
        return result;
    }
}