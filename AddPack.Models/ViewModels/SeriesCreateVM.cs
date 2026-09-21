using AddPack.Models.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AddPack.Models.ViewModels;

public class SeriesCreateVM
{
    [Required]
    [Display(Name = "Nazwa serii")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Slug (link)")]
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Opis serii")]
    public string Description { get; set; } = string.Empty;

    [MaxFileSize(5)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".webp", ".gif"])]
    [Display(Name = "Zdjęcie serii")]
    public IFormFile? Image { get; set; }

    [Required]
    [Display(Name = "Seria aktywna")]
    public bool IsActive { get; set; }

    [Display(Name = "Kolejność wyświetlania")]
    public int? SortOrder { get; set; }
}
