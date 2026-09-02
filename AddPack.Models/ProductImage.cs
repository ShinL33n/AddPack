using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddPack.Models;

public class ProductImage
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProductVariantId { get; set; }
    [ForeignKey(nameof(ProductVariantId))]
    public required ProductVariant ProductVariant { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Główne zdjęcie")]
    public bool IsPrimary { get; set; } = false;

    [Display(Name = "Tekst alternatywny")]
    public string AltText { get; set; } = string.Empty;

    [Display(Name = "Kolejność wyświetlania")]
    public int SortOrder { get; set; } // auto przypisanie przy dodawaniu nowego zdjęcia, jeśli nie podano wartości
    public DateTime CreatedAt { get; set; }
}
