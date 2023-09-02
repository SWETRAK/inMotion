using System.Runtime.Serialization;

namespace IMS.Shared.Models.Exceptions;

public class InvalidUserGuidStringException: Exception
{
    public InvalidUserGuidStringException()
    {
    }

    protected InvalidUserGuidStringException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidUserGuidStringException(string message) : base(message)
    {
    }

    public InvalidUserGuidStringException(string message, Exception innerException) : base(message, innerException)
    {
    }
}