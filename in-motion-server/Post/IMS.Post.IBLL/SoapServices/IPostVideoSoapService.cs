using System.ServiceModel;
using IMS.Post.Models.Models.Soap;

namespace IMS.Post.IBLL.SoapServices;

[ServiceContract]
public interface IPostVideoSoapService
{
    [OperationContract]
    Task SaveUploadedVideo(UploadVideosMetaData uploadVideosMetaData);
}