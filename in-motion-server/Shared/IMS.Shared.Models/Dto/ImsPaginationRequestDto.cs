namespace IMS.Shared.Models.Dto;

public class ImsPaginationRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}