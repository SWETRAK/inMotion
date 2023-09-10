using System.Runtime.Serialization;

namespace IMS.Shared.Models.Exceptions;

public class InvalidGuidStringException: Exception
{
    public InvalidGuidStringException()
    {
    }

    protected InvalidGuidStringException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidGuidStringException(string message) : base(message)
    {
    }

    public InvalidGuidStringException(string message, Exception innerException) : base(message, innerException)
    {
    }
}