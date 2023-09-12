using PostEntity = IMS.Post.Domain.Entities.Post.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostRepository: IDisposable
{
    Task<IList<PostEntity>> GetPublicFormDayPaginatedAsync(DateTime dateTime, int pageNumber, int pageSize = 20);
    
    Task<PostEntity> GetByExternalAuthorIdAsync(DateTime dateTime, Guid externalAuthorId);

    Task<PostEntity> GetByIdAndAuthorIdAsync(DateTime dateTime, Guid postId, Guid userId);

    Task<PostEntity> GetByIdAsync(Guid postId);

    Task SaveAsync();

    Task<IList<PostEntity>> GetFriendsPublicFromDayPaginatedAsync(DateTime dateTime,
        IEnumerable<Guid> friendIds, int pageNumber, int pageSize = 20);
}