namespace IMS.Post.Models.Dto.Incoming;

public class CreatePostRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public NewPostLocalizationDto Localization { get; set; }
}