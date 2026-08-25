using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class Series
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name="Nazwa serii")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Slug (link)")]
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Opis serii")]
    public string? Description { get; set; }

    [Display(Name = "Seria nadrzędna")]
    public int? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Series? SeriesParentId { get; set; }

    [Display(Name = "Zdjęcie")]
    public string? Image { get; set; }

    [Required]
    [Display(Name = "Seria aktywna")]
    public bool IsActive { get; set; }

    [Display(Name = "Kolejność wyświetlania")]
    public int? SortOrder { get; set; } // auto increment?
    public DateTime CreatedAt { get; set; }

}
