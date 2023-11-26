using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostIterationNotFoundException: Exception
{
    public PostIterationNotFoundException()
    {
    }

    protected PostIterationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostIterationNotFoundException(string message) : base(message)
    {
    }

    public PostIterationNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}