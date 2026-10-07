using AddPack.Models;

namespace AddPack.Business.Services.IServices;

public interface ICategoryService
{
    // Get
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetCategoriesByIdAsync(List<Guid> ids);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();

    // Create
    Task<Category> CreateCategoryAsync(Category category);

    // Update
    Task<Category> UpdateCategoryAsync(Category category);
    Task<int> UpdateCategoriesActiveStatusAsync(List<Guid> ids, bool value);

    // Delete
    Task<int> DeleteCategoriesAsync(List<Guid> ids);

    // Utils
    Task<int> GetMaxSortOrderAsync(Guid? parentId = null);
    Task<bool> IsNameUniqueAsync(string name, Guid? id = null);

}
