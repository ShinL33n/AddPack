using AddPack.Business.Services;
using AddPack.DataAccess.Data;
using AddPack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddPack.Web.Controllers;

public class SeriesController : Controller
{
    private readonly ISeriesService _seriesService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SeriesController(ISeriesService seriesService,
                            IWebHostEnvironment webHostEnvironment)
    {
        _seriesService = seriesService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var series = await _seriesService.GetAllSeriesAsync();

        return View(series);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Series series, IFormFile? file)
    {
        series.Id = Guid.NewGuid();
        series.CreatedAt = DateTime.UtcNow;

        if (file != null)
        {
            series.Image = await AddImageAsync(file, series.Id, series.Name);
        }

        if (series.SortOrder == null)
        {
            var maxSortOrder = await _seriesService.GetMaxSortOrderAsync();
            series.SortOrder = maxSortOrder + 1;
        }

        // Add validator

        if (!String.IsNullOrEmpty(series.Name) && !await _seriesService.IsNameUniqueAsync(series.Name))
        {
            ModelState.AddModelError("Name", "Seria o tej nazwie już istnieje.");
        }

        if (!String.IsNullOrEmpty(series.Slug) && !await _seriesService.IsNameUniqueAsync(series.Slug))
        {
            ModelState.AddModelError("Slug", "Slug o tej nazwie już istnieje.");
        }

        if (ModelState.IsValid)
        {
            var seriesCreated = await _seriesService.CreateSeriesAsync(series);
            TempData["Success"] = $"Seria {seriesCreated.Name} została utworzona pomyślnie.";

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    //[HttpGet]
    //public async Task<IActionResult> Edit(Guid? id)
    //{
    //    if (!id.HasValue || id == null)
    //    {
    //        return NotFound();
    //    }

    //    var series = await _seriesService.GetSeriesByIdAsync(id.Value);

    //    if(series == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(series);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> EditAsync(Series series, IFormFile? file)
    //{
    //    if (file != null)
    //    {
    //        series.Image = await AddImageAsync(file, series.Id, series.Name);
    //    }

    //    if (series.SortOrder == null)
    //    {
    //        var maxSortOrder = await _seriesService.GetMaxSortOrderAsync();
    //        series.SortOrder = maxSortOrder + 1;
    //    }

    //    if (!String.IsNullOrEmpty(series.Name) && !await _seriesService.IsNameUniqueAsync(series.Name, series.Id))
    //    {
    //        ModelState.AddModelError("", "Seria o tej nazwie już istnieje.");
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        var seriesUpdated = await _seriesService.UpdateSeriesAsync(series);
    //        TempData["Success"] = $"Seria {seriesUpdated.Name} została pomyślnie zaktualizowana.";

    //        return RedirectToAction("Index");
    //    }

    //    return View();
    //}

    [HttpGet]
    public async Task<IActionResult> Edit(string? slug)
    {
        if (string.IsNullOrEmpty(slug))
        {
            return NotFound();
        }

        var series = await _seriesService.GetSeriesBySlugAsync(slug);

        if (series == null)
        {
            return NotFound();
        }

        return View(series);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Series series, IFormFile? file)
    {
        if (file != null)
        {
            series.Image = await AddImageAsync(file, series.Id, series.Name);
        }

        if (series.SortOrder == null)
        {
            var maxSortOrder = await _seriesService.GetMaxSortOrderAsync();
            series.SortOrder = maxSortOrder + 1;
        }

        // Add validator

        if (!String.IsNullOrEmpty(series.Name) && !await _seriesService.IsNameUniqueAsync(series.Name, series.Id))
        {
            ModelState.AddModelError("", "Seria o tej nazwie już istnieje.");
        }

        if (!String.IsNullOrEmpty(series.Slug) && !await _seriesService.IsNameUniqueAsync(series.Slug, series.Id))
        {
            ModelState.AddModelError("", "Slug o tej nazwie już istnieje.");
        }

        if (ModelState.IsValid)
        {
            var seriesUpdated = await _seriesService.UpdateSeriesAsync(series);
            TempData["Success"] = $"Seria {seriesUpdated.Name} została pomyślnie zaktualizowana.";

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (!id.HasValue || id == null)
        {
            return NotFound();
        }

        var series = await _seriesService.GetSeriesByIdAsync(id.Value);

        if (series == null)
        {
            return NotFound();
        }

        return View(series);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(Guid id)
    {
        await _seriesService.DeleteSeriesAsync(id);
        TempData["Success"] = "Series deleted successfully";
        return RedirectToAction("Index");
    }

    //[HttpGet]
    //public async Task<IActionResult> Delete(string slug)
    //{
    //    if (string.IsNullOrEmpty(slug))
    //    {
    //        return NotFound();
    //    }

    //    var series = await _seriesService.GetSeriesBySlugAsync(slug);

    //    if (series == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(series);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //[ActionName("Delete")]
    //public async Task<IActionResult> DeletePOST(string slug)
    //{
    //    await _seriesService.DeleteSeriesBySlugAsync(slug);
    //    TempData["Success"] = "Series deleted successfully";
    //    return RedirectToAction(nameof(Index));
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Archive(Guid id)
    {
        var seriesToArchive = await _seriesService.GetSeriesByIdAsync(id);
        if(seriesToArchive != null)
        {
            seriesToArchive.IsActive = false;
            await _seriesService.UpdateSeriesAsync(seriesToArchive);
        }

        return RedirectToAction(nameof(Index));
    }



    public async Task<string> AddImageAsync(IFormFile file, Guid guid, string name)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;

        string fileName = guid.ToString() + "_" + name + Path.GetExtension(file.FileName);
        string seriesPath = Path.Combine("images", "series");
        string finalPath = Path.Combine(wwwRootPath, seriesPath);

        if (!Directory.Exists(finalPath))
        {
            Directory.CreateDirectory(finalPath);
        }

        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return Path.Combine(@"\", seriesPath, fileName).Replace("\\", "/");
    }
}
