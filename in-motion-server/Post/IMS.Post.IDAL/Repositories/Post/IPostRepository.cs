using PostEntity = IMS.Post.Domain.Entities.Post.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostRepository: IDisposable
{
    Task<IEnumerable<PostEntity>> GetPublicFormDayPaginatedAsync(DateTime dateTime, int pageNumber, int pageSize = 20);
    
    Task<PostEntity> GetByExternalAuthorIdAsync(DateTime dateTime, Guid externalAuthorId);

    Task<PostEntity> GetByIdAndAuthorIdAsync(DateTime dateTime, Guid postId, Guid userId);

    Task SaveAsync();

    Task<IEnumerable<Domain.Entities.Post.Post>> GetFriendsPublicFromDayPaginatedAsync(DateTime dateTime,
        IEnumerable<Guid> friendIds, int pageNumber, int pageSize = 20);
}