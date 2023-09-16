namespace IMS.Post.IDAL.Repositories.Post;

public interface IPostVideoRepository: IDisposable
{
    Task SaveAsync();
}