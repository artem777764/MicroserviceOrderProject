using Backend.DTOs.UserDTOs;
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
}