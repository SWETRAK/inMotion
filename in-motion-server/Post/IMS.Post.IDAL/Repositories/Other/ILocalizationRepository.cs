using IMS.Post.Domain.Entities.Other;

namespace IMS.Post.IDAL.Repositories.Other;

public interface ILocalizationRepository: IDisposable
{
    Task<Localization> GetByCoordinatesOrNameAsync(double latitude, double longitude, string name);
    Task SaveAsync();
}