using IMS.Shared.Models.Dto.Other;

namespace IMS.Shared.Models.Dto.Post;

public class CreatePostDto
{
    public string Description { get; set; }
    public string Title { get; set; }

    public IEnumerable<string> Tags { get; set; }

    public LocalizationDto Localization { get; set; }
}