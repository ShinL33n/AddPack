using AddPack.Models.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models.ViewModels;

public class SeriesEditVM
{
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "Nazwa serii")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Slug (link)")]
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Opis serii")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Zdjęcie serii")]
    public string? Image { get; set; }

    [Required]
    [Display(Name = "Seria aktywna")]
    public bool IsActive { get; set; }

    [Display(Name = "Kolejność wyświetlania")]
    public int? SortOrder { get; set; }

    [Display(Name = "Utworzono")]
    public DateTime CreatedAt { get; set; }

    [MaxFileSize(5)]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".webp", ".gif"])]
    [Display(Name = "Nowe zdjęcie serii")]
    public IFormFile? NewImage { get; set; }


    public SeriesEditVM() { }

    public SeriesEditVM(Series series)
    {
        Id = series.Id;
        Name = series.Name;
        Slug = series.Slug;
        Description = series.Description;
        Image = series.Image;
        IsActive = series.IsActive;
        SortOrder = series.SortOrder;
        CreatedAt = series.CreatedAt;
    }

}