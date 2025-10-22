namespace UserService.DTOs;

public record ErrorDTO
{
    public required string Code { get; set; }
    public required string Message { get; set; }
}