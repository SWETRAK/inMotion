namespace IMS.Shared.Messaging.Messages;

public class ImsBaseMessage<T>
{
    public bool Error { get; set; } = false;
    public string ErrorMessage { get; set; } = null;
    public T Data { get; set; }
}