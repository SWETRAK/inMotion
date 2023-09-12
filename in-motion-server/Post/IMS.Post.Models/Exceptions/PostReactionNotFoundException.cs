using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostReactionNotFoundException: Exception
{
    public PostReactionNotFoundException()
    {
    }

    protected PostReactionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostReactionNotFoundException(string message) : base(message)
    {
    }

    public PostReactionNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}