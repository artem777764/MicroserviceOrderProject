using Backend.DTOs.UserDTOs;
using UserService.DTOs.UserDataDTOs;
using UserService.Models.Entities;

namespace UserService.Extensions;

public static class UserExtensions
{
    public static UserEntity ToEntity(this CreateUserDTO createUserDTO, string passwordHash) => new UserEntity()
    {
        Email = createUserDTO.Email,
        Login = createUserDTO.Login,
        PasswordHash = passwordHash,
    };

    public static GetUserDTO ToDTO(this UserEntity userEntity) => new GetUserDTO()
    {
        Id = userEntity.Id,
        Email = userEntity.Email,
        Login = userEntity.Login,
        Surname = userEntity.UserData?.Surname ?? "Не указано",
        Name = userEntity.UserData?.Name ?? "Не указано",
        Patronymic = userEntity.UserData?.Patronymic ?? "Отсутствует",
        CreatedAt = DateTime.UtcNow,
        UpdateddAt = DateTime.UtcNow,
        Roles = userEntity.UserRoles.Select(ur => ur.Role.ToGetDTO()).ToList(),
    };
}