namespace UserService.DTOs.UserRoleDTOs;

public record RemoveUserRoleDTO
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}