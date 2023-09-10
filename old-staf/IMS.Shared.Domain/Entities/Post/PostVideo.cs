using IMS.Shared.Domain.Consts;
using IMS.Shared.Domain.Entities.Bases;

namespace IMS.Shared.Domain.Entities.Post;

public class PostVideo: BaseVideo
{
    public PostVideoType Type { get; set; }

    public virtual Post PostFront { get; set; }
    public Guid PostFrontId { get; set; }
    
    public virtual Post PostRear { get; set; }
    public Guid PostRearId { get; set; }

    public PostVideo()
    {
        CreationDate = DateTime.UtcNow;
        LastEditionDate = DateTime.UtcNow;
    }
}