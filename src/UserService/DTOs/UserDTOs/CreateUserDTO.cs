namespace Backend.DTOs.UserDTOs;

public record CreateUserDTO
{
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}