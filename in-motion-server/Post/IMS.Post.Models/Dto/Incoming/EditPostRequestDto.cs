namespace IMS.Post.Models.Dto.Incoming;

public class EditPostRequestDto
{
    public string Title { get; set; } // Length: 256
    public string Description { get; set; } // Length: 2048
}