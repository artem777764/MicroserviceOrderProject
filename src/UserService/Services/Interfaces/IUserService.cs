using Backend.DTOs.UserDTOs;
using UserService.DTOs;

namespace UserService.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateUserDTO createUserDTO);
}