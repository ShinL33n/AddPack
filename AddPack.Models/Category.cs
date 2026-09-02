using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Display(Name = "Kategoria nadrzędna")]
    public Guid? ParentId { get; set; }
    [ForeignKey(nameof(ParentId))]
    public Category? CategoryParent { get; set; }

    [Required]
    [Display(Name = "Nazwa kategorii")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Opis kategorii")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Slug (link)")]
    public string Slug { get; set; } = string.Empty;

    [Display(Name = "Ikona serii")]
    public string? Image { get; set; }

    [Required]
    [Display(Name = "Kategoria aktywna")]
    public bool IsActive { get; set; }

    [Display(Name = "Kolejność wyświetlania")]
    public int? SortOrder { get; set; } // auto increment if null - chyba tu nie bo bedzie kilka subkategorii

    [Display(Name = "Utworzono")]
    public DateTime CreatedAt { get; set; }

    public ICollection<Category> Subcategories { get; set; } = [];
}
