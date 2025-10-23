namespace Backend.DTOs.UserDTOs;

public record GetRoleDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}