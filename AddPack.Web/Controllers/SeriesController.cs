using AddPack.DataAccess.Data;
using AddPack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddPack.Web.Controllers;

public class SeriesController : Controller
{
    private readonly ApplicationDbContext _dbcontext;

    public SeriesController(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public IActionResult Index()
    {
        var series = _dbcontext.Series.ToList();

        return View(series);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Series series)
    {
        series.CreatedAt = DateTime.UtcNow;

        _dbcontext.Series.Add(series);
        _dbcontext.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var series = _dbcontext.Series.Find(id);

        // need dto

        return View(series);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Series series)
    {
        _dbcontext.Series.Update(series);
        _dbcontext.SaveChanges();


        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var series = _dbcontext.Series.Find(id);

        // need dto

        return View(series);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public IActionResult DeletePOST(int id)
    {
        var series = _dbcontext.Series.Find(id);
        _dbcontext.Series.Remove(series);
        _dbcontext.SaveChanges();


        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Archive(int id)
    {
        var series = _dbcontext.Series.Find(id);
        series.IsActive = false;
        _dbcontext.Series.Update(series);
        _dbcontext.SaveChanges();


        return RedirectToAction("Index");
    }
}
