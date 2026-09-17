using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AddPack.Models.Validation;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxBytes;
    public MaxFileSizeAttribute(int maxMegabytes) => _maxBytes = maxMegabytes * 1024L * 1024L;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is IFormFile file && file.Length > _maxBytes)
            return new ValidationResult($"Plik jest zbyt duży - maksymalnie {_maxBytes / 1024 / 1024} MB.");
        return ValidationResult.Success;

    }
}
