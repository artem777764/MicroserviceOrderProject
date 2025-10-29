using UserService.DTOs;
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

    public async Task<ApiResponseNoDataDTO> AppointRoleAsync(CreateUserRoleDTO createUserRoleDTO)
    {
        ApiResponseNoDataDTOBuilder apiResponseNoDataDTOBuilder = new ApiResponseNoDataDTOBuilder();
        await _userRoleRepository.AppointRoleAsync(createUserRoleDTO.ToEntity());
        return apiResponseNoDataDTOBuilder.SetSuccessful()
                                          .Build();
    }

    public async Task<ApiResponseNoDataDTO> RemoveRoleAsync(RemoveUserRoleDTO removeUserRoleDTO)
    {
        ApiResponseNoDataDTOBuilder apiResponseNoDataDTOBuilder = new ApiResponseNoDataDTOBuilder();
        await _userRoleRepository.RemoveRoleAsync(removeUserRoleDTO.ToEntity());
        return apiResponseNoDataDTOBuilder.SetSuccessful()
                                          .Build();
    }
}