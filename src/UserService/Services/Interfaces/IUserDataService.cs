using UserService.DTOs;
using UserService.DTOs.UserDataDTOs;

namespace UserService.Services.Interfaces;

public interface IUserDataService
{
    Task<ApiResponseDTO<IdDTO>> CreateUserDataAsync(Guid userId, CreateUserDataDTO createUserDataDTO);
}