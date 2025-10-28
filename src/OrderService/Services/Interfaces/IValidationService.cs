namespace OrderService.Services.Interfaces;

public interface IValidationService
{
    bool IsValidProductName(string? productName);
}