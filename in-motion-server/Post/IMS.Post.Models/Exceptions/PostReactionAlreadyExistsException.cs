using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostReactionAlreadyExistsException: Exception
{
    public PostReactionAlreadyExistsException()
    {
    }

    protected PostReactionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostReactionAlreadyExistsException(string message) : base(message)
    {
    }

    public PostReactionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}