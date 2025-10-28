using OrderService.DTOs;
using OrderService.DTOs.ItemDTOs;

namespace OrderService.Services.Interfaces;

public interface IItemService
{
    Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateItemDTO createItemDTO);
}