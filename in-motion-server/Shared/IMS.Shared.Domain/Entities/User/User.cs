namespace IMS.Shared.Domain.Entities.User;

public class User
{
    public Guid Id { get; set;}

    public virtual IEnumerable<Provider> Providers { get; set; }

    public string Nickname { get; set; }
    public string Bio { get; set; }

    public Guid ProfileVideoId { get; set; }
    public virtual UserProfileVideo ProfileVideo{ get; set; }
}