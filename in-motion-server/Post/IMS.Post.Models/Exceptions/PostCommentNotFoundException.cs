using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostCommentNotFoundException : Exception
{
    public PostCommentNotFoundException()
    {
    }

    protected PostCommentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostCommentNotFoundException(string message) : base(message)
    {
    }

    public PostCommentNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}