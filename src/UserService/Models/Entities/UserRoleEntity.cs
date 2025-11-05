namespace UserService.Models.Entities;

public class UserRoleEntity
{
    public required Guid UserId { get; set; }
    public required Guid RoleId { get; set; }

    public RoleEntity Role { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
}