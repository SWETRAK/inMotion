using System.Runtime.Serialization;

namespace IMS.Shared.Models.Exceptions;

public class NestedRabbitMqRequestException: Exception
{
    public NestedRabbitMqRequestException()
    {
    }

    protected NestedRabbitMqRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NestedRabbitMqRequestException(string message) : base(message)
    {
    }

    public NestedRabbitMqRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}