namespace Backend.DTOs.UserDTOs;

public record GetUserDTO
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public string? Patronymic { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdateddAt { get; set; }
    public required List<GetRoleDTO> Roles { get; set; }
}