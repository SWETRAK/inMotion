using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.IBLL.Services;

public interface IPostVideoService
{
    Task SaveUploadedVideos(UploadVideosMetaDataDto uploadVideosMetaDataDto);
}