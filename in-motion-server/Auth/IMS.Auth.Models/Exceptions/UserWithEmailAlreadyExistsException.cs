namespace IMS.Auth.Models.Exceptions;

public class UserWithEmailAlreadyExistsException: Exception
{
    public string Email { get; set; }

    public UserWithEmailAlreadyExistsException()
    {
    }

    public UserWithEmailAlreadyExistsException(string email)
    {
        Email = email;
    }
}