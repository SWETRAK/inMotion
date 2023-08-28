namespace IMS.User.Domain.Entities;

public class UserMetas
{
    public Guid Id { get; set; }

    public Guid UserExternalId { get; set; }

    public string Bio { get; set; }
    
    // This should be nullable because of null user profile video in registration 
    public Guid? ProfileVideoId { get; set; }
    public virtual UserProfileVideo ProfileVideo { get; set; }
}