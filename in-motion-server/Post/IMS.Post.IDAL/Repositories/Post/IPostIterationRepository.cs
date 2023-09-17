using IMS.Post.Domain.Entities.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostIterationRepository: IDisposable
{
    Task<PostIteration> GetById(Guid id);
    Task<PostIteration> GetNewest();
}