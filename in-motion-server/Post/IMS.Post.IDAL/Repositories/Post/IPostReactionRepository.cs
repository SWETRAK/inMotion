using IMS.Post.Domain.Entities.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostReactionRepository: IDisposable
{
    Task<PostReaction> GetByAuthorIdAndPostIdAsync(Guid authorId, Guid postId);

    Task<PostReaction> GetByIdAndUserIdAndPostIdAsync(Guid id, Guid userId, Guid postId);
    
    Task<PostReaction> GetByIdAndAuthorId(Guid id, Guid authorId);

    Task<IList<PostReaction>> GetRangeByPostIdAsync(Guid postId);

    Task AddAsync(PostReaction postReaction);
    
    Task<long> GetRangeByPostIdCountAsync(Guid postId);
    
    Task<PostReaction> GetByIdAsync(Guid id);
    
    
    void Delete(PostReaction postReaction);
    
    Task SaveAsync();
}