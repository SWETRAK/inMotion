using IMS.Post.IBLL.SoapServices;

namespace IMS.Post.BLL.SoapServices;

public class PostVideoService: IPostVideoService
{
    private readonly IBLL.Services.IPostVideoService _postVideoService;

    public PostVideoService(IBLL.Services.IPostVideoService postVideoService)
    {
        _postVideoService = postVideoService;
    }

    public async Task SaveUploadedVideo()
    {
        await _postVideoService.SaveUploadedVideos(null);
    }
}