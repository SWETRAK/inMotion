using System.Runtime.Serialization;

namespace IMS.User.Models.Exceptions;

public class NotFoundVideoException : Exception
{
    public NotFoundVideoException()
    {
    }

    protected NotFoundVideoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundVideoException(string message) : base(message)
    {
    }

    public NotFoundVideoException(string message, Exception innerException) : base(message, innerException)
    {
    }
}