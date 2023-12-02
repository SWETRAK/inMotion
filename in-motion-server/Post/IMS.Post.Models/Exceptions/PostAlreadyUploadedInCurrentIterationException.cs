using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostAlreadyUploadedInCurrentIterationException: Exception
{
    public PostAlreadyUploadedInCurrentIterationException()
    {
    }

    protected PostAlreadyUploadedInCurrentIterationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostAlreadyUploadedInCurrentIterationException(string message) : base(message)
    {
    }

    public PostAlreadyUploadedInCurrentIterationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}