using IMS.Post.Domain.Entities.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostCommentRepository: IDisposable
{
    Task<PostComment> GetByIdAsync(Guid id);
    Task<PostComment> GetByIdAndAuthorIdAndPostIdAsync(Guid id, Guid authorId, Guid postId);
    
    Task<IList<PostComment>> GetRangeByPostIdPaginatedAsync(Guid postId, int pageNumber, int pageSize);
    
    Task<long> GetRangeByPostIdCountAsync(Guid postId);

    Task<PostComment> GetByIdAndAuthorIdAsync(Guid id, Guid authorId);
    
    Task SaveAsync();
    void Delete(PostComment postComment);
}