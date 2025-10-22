namespace UserService.DTOs;

public record ApiResponseDTO<T>
{
    public required bool Success { get; set; }
    public T? Data { get; set; }
    public ErrorDTO? Error { get; set; }
}