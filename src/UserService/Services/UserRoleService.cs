using UserService.DTOs.UserRoleDTOs;
using UserService.Extensions;
using UserService.Repositories.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task AppointRoleAsync(CreateUserRoleDTO createUserRoleDTO)
    {
        await _userRoleRepository.AppointRoleAsync(createUserRoleDTO.ToEntity());
    }

    public async Task RemoveRoleAsync(RemoveUserRoleDTO removeUserRoleDTO)
    {
        await _userRoleRepository.RemoveRoleAsync(removeUserRoleDTO.ToEntity());
    }
}