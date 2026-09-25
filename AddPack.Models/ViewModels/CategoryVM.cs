using Microsoft.AspNetCore.Mvc.Rendering;

namespace AddPack.Models.ViewModels;

public class CategoryVM
{
    public Category Category { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }

}
