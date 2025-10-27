namespace UserService.DTOs.UserRoleDTOs;

public record CreateUserRoleDTO
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}