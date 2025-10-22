using Backend.DTOs.UserDTOs;
using UserService.DTOs.UserDataDTOs;
using UserService.Models.Entities;

namespace UserService.Extensions;

public static class UserDataExtensions
{
    public static UserDataEntity ToEntity(this CreateUserDataDTO createUserDataDTO, Guid userId) => new UserDataEntity()
    {
        Id = userId,
        Surname = createUserDataDTO.Surname,
        Name = createUserDataDTO.Name,
        Patronymic = createUserDataDTO.Patronymic,
        CreatedAt = DateTime.UtcNow,
        UpdateddAt = DateTime.UtcNow,
    };
}