namespace UserService.DTOs.UserDTOs;

public record LoginDTO
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}