namespace UserService.DTOs.UserDataDTOs;

public record CreateUserDataDTO
{
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public string? Patronymic { get; set; }
}