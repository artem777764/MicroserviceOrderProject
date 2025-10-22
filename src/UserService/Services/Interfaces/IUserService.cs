using Backend.DTOs.UserDTOs;
using UserService.DTOs;
using UserService.DTOs.UserDataDTOs;

namespace UserService.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateUserDTO createUserDTO);
    Task<ApiResponseDTO<GetUserDTO>> GetUserByIdAsync(Guid userId);
    Task<ApiResponseDTO<List<GetUserDTO>>> GetAllAsync();
}