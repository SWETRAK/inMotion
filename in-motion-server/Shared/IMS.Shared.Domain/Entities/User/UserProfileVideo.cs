using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.User;

public class UserProfileVideo: BaseVideo
{ 
    public UserProfileVideo()
    {
        CreationDate = DateTime.UtcNow;
        LastEditionDate = DateTime.UtcNow;
    }
}