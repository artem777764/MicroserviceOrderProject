using Backend.DTOs.UserDTOs;
using UserService.DTOs;
using UserService.DTOs.UserDataDTOs;
using UserService.DTOs.UserDTOs;

namespace UserService.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateUserDTO createUserDTO);
    Task<ApiResponseDTO<GetUserDTO>> GetUserByIdAsync(Guid userId);
    Task<ApiResponseDTO<List<GetUserDTO>>> GetAllAsync();
    Task<ApiResponseDTO<GetLoginUserDTO>> LoginUserAsync(LoginDTO loginDTO);
    Task RemoveByIdAsync(Guid userId);
    Task<ApiResponseDTO<GetLoginUserDTO>> SetRoleAsync(Guid userId, Guid roleId);
}