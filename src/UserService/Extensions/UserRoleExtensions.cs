using UserService.DTOs.UserRoleDTOs;
using UserService.Models.Entities;

namespace UserService.Extensions;

public static class UserRoleExtensions
{
    public static UserRoleEntity ToEntity(this CreateUserRoleDTO createUserRoleDTO)
    {
        return new UserRoleEntity()
        {
            UserId = createUserRoleDTO.UserId,
            RoleId = createUserRoleDTO.RoleId,
        };
    }

    public static UserRoleEntity ToEntity(this RemoveUserRoleDTO removeUserRoleDTO)
    {
        return new UserRoleEntity()
        {
            UserId = removeUserRoleDTO.UserId,
            RoleId = removeUserRoleDTO.RoleId,
        };
    }
}