namespace IMS.Shared.Models.Dto;

public class ImsHttpError
{
    public int Status { get; set; }
    public string ErrorMessage { get; set; }

    public string ErrorType { get; set; }
}