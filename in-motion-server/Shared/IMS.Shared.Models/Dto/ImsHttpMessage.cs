namespace IMS.Shared.Models.Dto;

public class ImsHttpMessage<T>
{
    public int Status { get; set; }
    public DateTime ServerResponseTime { get; set; }
    public DateTime ServerRequestTime { get; set; }
    public T Data { get; set; }
}