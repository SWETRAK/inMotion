namespace IMS.User.Domain.Entities;

public class UserMetas
{
    public Guid Id { get; set; }

    public Guid UserExternalId { get; set; }

    public string Bio { get; set; }
    public Guid? ProfileVideoId { get; set; }
    public virtual UserProfileVideo ProfileVideo { get; set; }
}