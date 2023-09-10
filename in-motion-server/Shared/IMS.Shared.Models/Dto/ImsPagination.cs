namespace IMS.Shared.Models.Dto;

public class ImsPagination<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public T Data { get; set; }
}