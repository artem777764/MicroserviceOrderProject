namespace UserService.Models.Entities;

public class UserDataEntity
{
    public Guid Id { get; set; }
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public string? Patronymic { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdateddAt { get; set; }

    public UserEntity User { get; set; } = null!;
}