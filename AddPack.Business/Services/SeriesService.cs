using AddPack.DataAccess.Data;
using AddPack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AddPack.Business.Services;

public interface ISeriesService
{
    Task<Series?> GetSeriesByIdAsync(Guid id);
    Task<Series?> GetSeriesBySlugAsync(string slug);
    Task<IEnumerable<Series>> GetAllSeriesAsync();
    Task<IEnumerable<Series>> GetAllActiveSeriesAsync();
    Task<Series> CreateSeriesAsync(Series series);
    Task<Series> UpdateSeriesAsync(Series series);
    Task DeleteSeriesAsync(Guid id);
    Task DeleteSeriesBySlugAsync(string slug);

    Task<int> GetMaxSortOrderAsync();
    Task<bool> IsNameUniqueAsync(string name, Guid? id = null);

}

public class SeriesService : ISeriesService
{
    private readonly ApplicationDbContext _dbContext;

    public SeriesService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Series?> GetSeriesByIdAsync(Guid id)
    {
        return await _dbContext.Series.FindAsync(id);
    }

    public async Task<Series?> GetSeriesBySlugAsync(string slug)
    {
        return await _dbContext.Series.FirstOrDefaultAsync(s => s.Slug == slug);
    }

    public async Task<IEnumerable<Series>> GetAllSeriesAsync()
    {
        return await _dbContext.Series.OrderBy(s => s.SortOrder).ToListAsync();
    }

    public async Task<IEnumerable<Series>> GetAllActiveSeriesAsync()
    {
        return await _dbContext.Series.Where(s => s.IsActive).OrderBy(s => s.SortOrder).ToListAsync();
    }

    public async Task<Series> CreateSeriesAsync(Series series)
    {
        _dbContext.Series.Add(series);
        await _dbContext.SaveChangesAsync();

        return series;
    }

    public async Task<Series> UpdateSeriesAsync(Series series)
    {
        _dbContext.Series.Update(series);
        await _dbContext.SaveChangesAsync();
        return series;
    }

    public async Task DeleteSeriesAsync(Guid id)
    {
        var series = _dbContext.Series.Find(id);

        if (series != null)
        {
            _dbContext.Series.Remove(series);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task DeleteSeriesBySlugAsync(string slug)
    {
        var series = _dbContext.Series.FirstOrDefault(s => s.Slug == slug);

        if (series != null)
        {
            _dbContext.Series.Remove(series);
            await _dbContext.SaveChangesAsync();
        }
    }




    public async Task<int> GetMaxSortOrderAsync()
    {
        return await _dbContext.Series.MaxAsync(s => (int?)s.SortOrder) ?? 0;
    }

    public async Task<bool> IsNameUniqueAsync(string name, Guid? id = null)
    {
        if (id.HasValue)
        {
            return !await _dbContext.Series.AnyAsync(s => s.Name == name && s.Id != id.Value);
        }
        else
        {
            return !await _dbContext.Series.AnyAsync(s => s.Name == name);
        }
    }
}
