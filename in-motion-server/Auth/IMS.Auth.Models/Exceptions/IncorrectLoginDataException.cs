namespace IMS.Auth.Models.Exceptions;

public class IncorrectLoginDataException: Exception
{
    public string Email { get; set; }

    public IncorrectLoginDataException()
    {
    }
    
    public IncorrectLoginDataException(string email)
    {
        Email = email;
    }
}