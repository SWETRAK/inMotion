using System.Runtime.Serialization;

namespace IMS.Friends.Models.Exceptions;

public class UsersNotFoundException: Exception
{
    public UsersNotFoundException()
    {
    }

    protected UsersNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UsersNotFoundException(string message) : base(message)
    {
    }

    public UsersNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}