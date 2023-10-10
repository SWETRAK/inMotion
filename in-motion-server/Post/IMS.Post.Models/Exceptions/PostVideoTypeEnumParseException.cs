using System.Runtime.Serialization;

namespace IMS.Post.Models.Exceptions;

public class PostVideoTypeEnumParseException: Exception
{
    public PostVideoTypeEnumParseException()
    {
    }

    protected PostVideoTypeEnumParseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public PostVideoTypeEnumParseException(string message) : base(message)
    {
    }

    public PostVideoTypeEnumParseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}