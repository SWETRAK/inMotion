using System.Runtime.Serialization;

namespace IMS.Friends.Models.Exceptions;

public class RabbitMqException : Exception
{
    public RabbitMqException()
    {
    }

    protected RabbitMqException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public RabbitMqException(string message) : base(message)
    {
    }

    public RabbitMqException(string message, Exception innerException) : base(message, innerException)
    {
    }
}