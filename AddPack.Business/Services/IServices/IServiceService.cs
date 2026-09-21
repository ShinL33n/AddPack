using AddPack.Models;
using System.Linq.Expressions;

namespace AddPack.Business.Services.IServices;

public interface ISeriesService
{
    Task<Series?> GetSeriesByIdAsync(Guid id);
    Task<Series?> GetSeriesBySlugAsync(string slug);
    Task<IEnumerable<Series>> GetAllSeriesAsync();
    Task<IEnumerable<Series>> GetAllActiveSeriesAsync();
    Task<Series> CreateSeriesAsync(Series series);
    Task<Series> UpdateSeriesAsync(Series series);
    Task<int> UpdateSelectedSeriesPropertyAsync<TProperty>(List<Guid> ids, Expression<Func<Series, TProperty>> propertySelector, TProperty value);

    Task DeleteSeriesAsync(Guid id);
    Task DeleteSeriesBySlugAsync(string slug);
    Task<int> DeleteSelectedSeriesAsync(List<Guid> ids);

    Task<int> GetMaxSortOrderAsync();
    Task<bool> IsNameUniqueAsync(string name, Guid? id = null);

}
