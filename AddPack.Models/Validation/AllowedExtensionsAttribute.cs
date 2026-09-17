using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AddPack.Models.Validation;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions;
    public AllowedExtensionsAttribute(string[] allowedExtensions) => _allowedExtensions = allowedExtensions;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is IFormFile file &&
            !_allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
        {
            return new ValidationResult($"Plik musi być jednym z dozwolonych typów: {_allowedExtensions.Aggregate((x, y) => x + ", " + y)}.");
        }
        return ValidationResult.Success;
    }
}
