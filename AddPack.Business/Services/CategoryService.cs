using AddPack.Business.Services.IServices;
using AddPack.DataAccess.Data;
using AddPack.Models;
using Microsoft.EntityFrameworkCore;

namespace AddPack.Business.Services;


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
            /* Wersja z zapytaniami do bazy w pętli, ale bez pobierania wszystkich kategorii do pamięci
            // Stwórz listę id kategorii do aktualizacji,
            // która później będzie aktualizowana w iteracji
            List<Guid> idsToUpdate = [.. ids.Distinct()], currentLevelIds = [.. ids.Distinct()];

            while (currentLevelIds.Count > 0)
            {
                // Znajdź wszystkie kategorie, które mają rodzica wśród kategorii do aktualizacji oraz same nie są
                // wśród początkowej listy kategorii do aktualizacji
                var childCategories = await _dbContext.Categories
                    .Where(c => c.ParentId.HasValue && currentLevelIds.Contains(c.ParentId.Value) && !ids.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync();

                idsToUpdate.AddRange(childCategories); // can i "add" nullable here?
                currentLevelIds = childCategories;
            }

            var updatedCount = await _dbContext.Categories
                .Where(c => idsToUpdate.Contains(c.Id))
                .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.IsActive, value));

            await transaction.CommitAsync();
            return updatedCount; */

            var allCategories = await _dbContext.Categories
                .Select(c => new { c.Id, c.ParentId })
                .ToListAsync();

            // Zbuduj mapę rodzic -> dzieci, żeby przeszukiwanie było O(1) per węzeł
            var childrenByParent = allCategories
                .Where(c => c.ParentId.HasValue)
                .GroupBy(c => c.ParentId!.Value)
                .ToDictionary(g => g.Key, g => g.Select(c => c.Id).ToList());

            // BFS całkowicie w pamięci — zero zapytań do bazy w tej pętli
            var idsToUpdate = new List<Guid>(ids.Distinct());
            var currentLevelIds = new List<Guid>(ids.Distinct());

            while (currentLevelIds.Count > 0)
            {
                var nextLevelIds = currentLevelIds
                    .SelectMany(parentId => childrenByParent[parentId])
                    .Where(childId => !idsToUpdate.Contains(childId))
                    .ToList();

                idsToUpdate.AddRange(nextLevelIds);
                currentLevelIds = nextLevelIds;
            }

            // Jedno zapytanie: finalny update na całym zebranym zbiorze
            int updatedCategoriesCount = await _dbContext.Categories
                .Where(c => idsToUpdate.Contains(c.Id))
                .ExecuteUpdateAsync(setters => setters.SetProperty(c => c.IsActive, value));

            int updatedProductsCount = await _dbContext.Products
                .Where(p => idsToUpdate.Contains(p.CategoryId))
                .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.IsActive, value));

            await transaction.CommitAsync();
            return updatedCategoriesCount;
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

    public async Task<int> GetMaxSortOrderAsync(Guid? parentId = null)
    {
        return parentId.HasValue ?
            await _dbContext.Categories.Where(c => c.ParentId == parentId).MaxAsync(c => (int?)c.SortOrder) ?? 0 :
            await _dbContext.Categories.Where(c => c.ParentId == null).MaxAsync(c => (int?)c.SortOrder) ?? 0;
    }

    public async Task<bool> IsNameUniqueAsync(string name, Guid? id = null)
    {
        if (id.HasValue)
        {
            return !await _dbContext.Categories.AnyAsync(c => c.Name == name && c.Id != id.Value);
        }
        else
        {
            return !await _dbContext.Categories.AnyAsync(c => c.Name == name);
        }
    }

    private Guid GetDefaultCategoryId()
    {
        return _dbContext.Categories
                .Where(c => c.Slug == "inne")
                .Select(c => c.Id)
                .FirstOrDefault();
    }
}
