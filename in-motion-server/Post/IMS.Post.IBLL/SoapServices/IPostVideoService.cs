using System.ServiceModel;

namespace IMS.Post.IBLL.SoapServices;

[ServiceContract]
public interface IPostVideoService
{
    [OperationContract]
    Task SaveUploadedVideo();
}