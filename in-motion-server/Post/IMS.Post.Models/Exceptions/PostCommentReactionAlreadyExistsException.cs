using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostCommentReactionAlreadyExistsException: Exception
{
    public PostCommentReactionAlreadyExistsException()
    {
    }

    protected PostCommentReactionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostCommentReactionAlreadyExistsException(string message) : base(message)
    {
    }

    public PostCommentReactionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}