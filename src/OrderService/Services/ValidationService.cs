using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class ValidationService : IValidationService
{
    public bool IsValidProductName(string? productName)
    {
        return !string.IsNullOrWhiteSpace(productName);
    }
}