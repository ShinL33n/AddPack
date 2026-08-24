using AddPack.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

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
}
