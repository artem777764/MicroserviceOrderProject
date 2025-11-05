using UserService.DTOs;
using UserService.DTOs.UserRoleDTOs;

namespace UserService.Services.Interfaces;

public interface IUserRoleService
{
    Task<ApiResponseNoDataDTO> AppointRoleAsync(CreateUserRoleDTO createUserRoleDTO);
    Task<ApiResponseNoDataDTO> RemoveRoleAsync(RemoveUserRoleDTO removeUserRoleDTO);
}