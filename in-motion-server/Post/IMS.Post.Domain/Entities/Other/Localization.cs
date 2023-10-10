namespace IMS.Post.Domain.Entities.Other;

public sealed class Localization
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}