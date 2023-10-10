using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostNotFoundException: Exception
{
    public PostNotFoundException()
    {
    }

    protected PostNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostNotFoundException(string message) : base(message)
    {
    }

    public PostNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}