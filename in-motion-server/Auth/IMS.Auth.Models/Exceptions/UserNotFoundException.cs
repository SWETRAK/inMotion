namespace IMS.Auth.Models.Exceptions;

public class UserNotFoundException: Exception
{
    public string Email { get; set; }

    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string email)
    {
        Email = email;
        
    }
}