using OrderService.DTOs;
using OrderService.DTOs.ItemDTOs;

namespace OrderService.Services.Interfaces;

public interface IItemService
{
    Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateItemDTO createItemDTO);
    Task<ApiResponseDTO<GetItemDTO>> GetItemByIdAsync(Guid itemId);
    Task<ApiResponseDTO<List<GetItemDTO>>> GetAllAsync();
    Task<ApiResponseNoDataDTO> RemoveByIdAsync(Guid itemId);
}