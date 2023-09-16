namespace IMS.Post.Models.Dto.Incoming;

public class UploadedVideoMetaDataDto
{
    public string PostId { get; set; }
    public string BucketName { get; set; }
    public string BucketLocation { get; set; }
    public string Filename { get; set; }
    public string AuthorId { get; set; }
    public string ContentType { get; set; }

    public string Type { get; set; }
}