using UserService.DTOs.UserRoleDTOs;

namespace UserService.Services.Interfaces;

public interface IUserRoleService
{
    Task AppointRoleAsync(CreateUserRoleDTO createUserRoleDTO);
    Task RemoveRoleAsync(RemoveUserRoleDTO removeUserRoleDTO);
}