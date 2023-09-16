using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostCommentReactionNotFoundException: Exception
{
    public PostCommentReactionNotFoundException()
    {
    }

    protected PostCommentReactionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostCommentReactionNotFoundException(string message) : base(message)
    {
    }

    public PostCommentReactionNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}