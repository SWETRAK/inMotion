using IMS.Post.Domain.Entities.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostCommentReactionRepository: IDisposable
{
    Task<IList<PostCommentReaction>> GetByPostCommentIdPaginatedAsync(Guid postCommentId, int pageNumber, int pageSize);
    Task<long> GetByPostCommentIdCountAsync(Guid postCommentId);
    Task<PostCommentReaction> GetByIdAndAuthorIdAsync(Guid id, Guid authorId);
    Task<PostCommentReaction> GetByIdAndAuthorIdAndPostCommentIdAsync(Guid id, Guid authorId, Guid postCommentId);
    void Remove(PostCommentReaction postCommentReaction);
    Task SaveAsync();
}