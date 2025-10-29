namespace OrderService.DTOs;

public record ApiResponseNoDataDTO
{
    public required bool Success { get; set; }
    public ErrorDTO? Error { get; set; }
}