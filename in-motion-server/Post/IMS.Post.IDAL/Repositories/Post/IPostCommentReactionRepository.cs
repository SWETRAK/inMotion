using IMS.Post.Domain.Entities.Post;

namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostCommentReactionRepository: IDisposable
{
    Task<IList<PostCommentReaction>> GetByPostCommentIdPaginatedAsync(Guid postCommentId);
    Task<long> GetByPostCommentIdCountAsync(Guid postCommentId);
    Task<PostCommentReaction> GetByIdAndAuthorIdAsync(Guid id, Guid authorId);
    Task<PostCommentReaction> GetByIdAndAuthorIdAndPostCommentIdAsync(Guid id, Guid authorId, Guid postCommentId);
    Task<PostCommentReaction> GetByAuthorIdAndPostCommentIdAsync(Guid authorId, Guid postCommentId);
    void Remove(PostCommentReaction postCommentReaction);
    Task SaveAsync();
}