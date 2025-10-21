namespace UserService.Models.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string PasswordHash { get; set; }

    public List<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    public UserDataEntity UserData { get; set; } = null!;
}