using IMS.Shared.Domain.Consts;

namespace IMS.Shared.Domain.Entities.User;

public class User
{
    public Guid Id { get; set;}

    public string Email { get; set; }
    public string HashedPassword { get; set; }
    
    public bool ConfirmedAccount { get; set; }
    public string ActivationToken { get; set; }
    
    public virtual IEnumerable<Provider> Providers { get; set; }

    public string Nickname { get; set; }
    public string Bio { get; set; }
    
    // This should be nullable because of null user profile video in registration 
    public Guid? ProfileVideoId { get; set; }
    public virtual UserProfileVideo ProfileVideo { get; set; }
    
    public Roles Role { get; set; }
}