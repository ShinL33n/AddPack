using AddPack.Business.Services.IServices;
using AddPack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

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

    #endregion
}
