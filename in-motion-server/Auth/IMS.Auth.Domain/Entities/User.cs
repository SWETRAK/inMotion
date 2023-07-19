using IMS.Auth.Domain.Consts;

namespace IMS.Auth.Domain.Entities;

public class User
{
    public Guid Id { get; set;}

    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string Nickname { get; set; }
    
    public bool ConfirmedAccount { get; set; }
    public string ActivationToken { get; set; }
    
    public virtual IEnumerable<Provider> Providers { get; set; }
    public Roles Role { get; set; }
}