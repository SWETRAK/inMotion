using System.Runtime.Serialization;

namespace IMS.Friends.Models.Exceptions;

public class InvalidFriendshipActionException: Exception
{
    public InvalidFriendshipActionException()
    {
    }

    protected InvalidFriendshipActionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidFriendshipActionException(string message) : base(message)
    {
    }

    public InvalidFriendshipActionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}