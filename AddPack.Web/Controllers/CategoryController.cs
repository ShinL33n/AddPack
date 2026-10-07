using AddPack.Business.Services;
using AddPack.Business.Services.IServices;
using AddPack.Models;
using AddPack.Models.DTOs;
using AddPack.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AddPack.Web.Controllers;

public class CategoryController : Controller
{
    // Logika obługi kategorii wraz z przedmotami:
    // (zastosować potem także do serii)
    //
    // Usuwanie
    // - Usunięcie kategorii nigdy nie usuwa przedmiotów z nią powiązanych
    // - Usunięcie kategorii nie usuwa także podkategorii, ale podkategorie zmieniają nadkategorie (rodzica) na kategorię "Inne"
    // - Kategoria "Inne" podlega innym zasadom i nie może zostać usunięta, (następną kwestię jeszcze przemyśleć) ale może być wyłączona
    // - Przy kategorii najniższego poziomu (bez podkategorii) przedmioty powiązane zostają stricte przeniesione do kategorii "Inne"
    // 
    // Wyłączenie / archiwizacja
    // - Wyłączenie kategorii wyłącza także subkategorie i przedmioty z nią powiązane
    // - Wyłączone kategorie i przedmioty nie są widoczne w witrynie, ani dostępne dla zwykłego użytkownika

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(Guid? id = null)
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        if (id == null)
        {
            CategoryVM categoryVM = new()
            {
                Category = new Category(),
                CategoryList = categories
                    .OrderBy(c => c.CreatedAt)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
            };

            return View(categoryVM);
        }
        else
        {
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category == null) return NotFound();

            CategoryVM categoryVM = new()
            {
                Category = category,
                CategoryList = categories
                    .OrderBy(c => c.CreatedAt)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
            };
            return View(categoryVM);
        }

    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(CategoryVM categoryVM)
    {
        Guid? id = categoryVM.Category.Id == Guid.Empty ? null : categoryVM.Category.Id;

        if (!String.IsNullOrEmpty(categoryVM.Category.Name) && !await _categoryService.IsNameUniqueAsync(categoryVM.Category.Name, id))
        {
            ModelState.AddModelError("Category.Name", "Kategoria o tej nazwie już istnieje.");
        }

        if (!String.IsNullOrEmpty(categoryVM.Category.Slug) && !await _categoryService.IsNameUniqueAsync(categoryVM.Category.Slug, id))
        {
            ModelState.AddModelError("Category.Slug", "Slug o tej nazwie już istnieje.");
        }

        if (ModelState.IsValid)
        {
            string successMessage;

            // Check if category is being created or updated
            if (categoryVM.Category.Id == Guid.Empty || categoryVM.Category.Id == null)
            {
                categoryVM.Category.Id = Guid.NewGuid();
                categoryVM.Category.CreatedAt = DateTime.UtcNow;

                if (categoryVM.Category.SortOrder == null)
                {
                    int maxSortOrder = await _categoryService.GetMaxSortOrderAsync(categoryVM.Category.ParentId);
                    categoryVM.Category.SortOrder = maxSortOrder + 1;
                }

                await _categoryService.CreateCategoryAsync(categoryVM.Category);
                successMessage = "utworzona";
            }
            else
            {
                // Handle the case where the category is being turned inactive/active
                // and ensure that the subcategories and items are also updated accordingly

                await _categoryService.UpdateCategoryAsync(categoryVM.Category);
                successMessage = "zaktualizowana";
            }

            TempData["Success"] = $"Kategoria została pomyślnie {successMessage}.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            categoryVM.CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(categoryVM);
        }
    }


    #region API_CALLS
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        List<CategoryTableDTO> categoryDtos = categories.Select(c => new CategoryTableDTO
        {
            Id = c.Id,
            ParentId = c.ParentId,
            Name = c.Name,
            Description = c.Description,
            Slug = c.Slug,
            IsActive = c.IsActive,
            SortOrder = c.SortOrder,
            CreatedAt = c.CreatedAt
        }).ToList();

        return Json(new { data = categoryDtos });
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    // Make it bulk-delete friendly
    public async Task<IActionResult> Delete(List<Guid> ids)
    {
        if (ids.Count == 0)
        {
            return Json(new { success = false, message = "Invalid ID(s)" });
        }

        var categories = await _categoryService.GetCategoriesByIdAsync(ids);

        if (!categories.Any())
        {
            return Json(new { success = false, message = "Error while deleting" });
        }


        int deletedCategories = await _categoryService.DeleteCategoriesAsync(ids);

        if(deletedCategories == 0) 
            return Json(new { success = false, message = "0 categories deleted." });

        return Json(new { success = true, message = "Delete Successful" });
    }

    [HttpPatch]
    [ValidateAntiForgeryToken]
    // Make it bulk-update friendly - checked
    public async Task<IActionResult> Activation(List<Guid> ids, bool? value = null)
    {
        if (ids.Count == 0)
        {
            return Json(new { success = false, message = "Invalid ID(s)" });
        }

        var categories = await _categoryService.GetCategoriesByIdAsync(ids);

        if (!categories.Any())
        {
            return Json(new { success = false, message = "Error while activating/deactivating" });
        }

        bool updateValue = value == null ? !categories.First().IsActive : value.Value;

        await _categoryService.UpdateCategoriesActiveStatusAsync(ids, updateValue);

        string action = updateValue ? "Activation" : "Deactivation";
        return Json(new { success = true, message = $"{action} Successful" });
    }

    #endregion
}
