using PostEntity = IMS.Post.Domain.Entities.Post.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostRepository: IDisposable
{
    Task<IList<PostEntity>> GetPublicFormIterationPaginatedAsync(Guid postIterationId, int pageNumber,
        int pageSize = 20);
    
    Task<PostEntity> GetByExternalAuthorIdAsync(Guid postIterationId, Guid externalAuthorId);

    Task<PostEntity> GetByIdAndAuthorIdAsync(Guid postIterationId, Guid postId, Guid userId);

    Task<PostEntity> GetByIdAndAuthorIdAsync(Guid postId, Guid userId);

    Task<PostEntity> GetByIdAsync(Guid postId);

    Task AddAsync(PostEntity post);

    Task SaveAsync();

    Task<IList<PostEntity>> GetFriendsPublicAsync(Guid postIterationId,
        IEnumerable<Guid> friendIds);
}