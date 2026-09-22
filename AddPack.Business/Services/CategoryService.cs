using AddPack.DataAccess.Data;
using AddPack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AddPack.Business.Services;

public interface ICategoryService
{
    // Get
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();

    // Create
    Task<Category> CreateCategoryAsync(Category category);

    // Update
    Task<Category> UpdateCategoryAsync(Category category);
    Task<int> UpdateCategoriesActiveStatusAsync(List<Guid> ids, bool value);

    // Delete
    Task<int> DeleteCategoriesAsync(List<Guid> ids);

    // Utils
    Task<int> GetMaxSortOrderAsync();
    Task<bool> IsNameUniqueAsync(string name, Guid? id = null);

}

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _dbContext.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }


    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }


    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<int> UpdateCategoriesActiveStatusAsync(
        List<Guid> ids, 
        bool value)
    {
        // - Wyłączenie kategorii wyłącza także subkategorie i przedmioty z nią powiązane
        // - Wyłączone kategorie i przedmioty nie są widoczne w witrynie, ani dostępne dla zwykłego użytkownika

        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            // !!Będzie działać tylko dla 1 poziomu w dół, zmienić na dowolną ilość poziomów!!
            
            // Znajdź kategorie, które mają rodzica,
            // który jest wśród kategorii do aktualizacji
            // lub same są wśród kategorii do aktualizacji
            await _dbContext.Categories
                .Where(c => c.ParentId.HasValue
                            && ids.Contains(c.ParentId.Value)
                            || ids.Contains(c.Id))
                .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.IsActive, value));

            await _dbContext.Products
                .Where(p => ids.Contains(p.CategoryId))
                .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsActive, value));


        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    public async Task<int> DeleteCategoriesAsync(List<Guid> ids)
    {
        var defaultId = GetDefaultCategoryId();
        var idsToDelete = ids.Where(id => id != defaultId).ToList(); // Exclude the default category from deletion

        if (idsToDelete.Count == 0)
            return 0;

        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            // Znajdź wszystkie kategorie, które mają rodzica,
            // ten rodzic jest wśród kategorii do usunięcia,
            // ale one same nie są wśród kategorii do usunięcia
            // i dla wszystkich takich kategorii ustaw ParentId na defaultId.
            await _dbContext.Categories
                .Where(c => c.ParentId.HasValue 
                            && idsToDelete.Contains(c.ParentId.Value) 
                            && !idsToDelete.Contains(c.Id))
                .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.ParentId, defaultId));

            // Następnie znajdź wszystkie produkty,
            // które mają kategorię wśród kategorii do usunięcia
            // i ustaw ich CategoryId na defaultId.
            await _dbContext.Products
                .Where(p => idsToDelete.Contains(p.CategoryId))
                .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.CategoryId, defaultId));

            // Na końcu usuń kategorie, które są wśród kategorii do usunięcia.
            var deletedCount = await _dbContext.Categories
                .Where(c => idsToDelete.Contains(c.Id))
                .ExecuteDeleteAsync();

            await transaction.CommitAsync();
            return deletedCount;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public Task<int> GetMaxSortOrderAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsNameUniqueAsync(string name, Guid? id = null)
    {
        throw new NotImplementedException();
    }

    private Guid GetDefaultCategoryId()
    {
        return _dbContext.Categories
                .Where(c => c.Slug == "inne")
                .Select(c => c.Id)
                .FirstOrDefault();
    }
}
