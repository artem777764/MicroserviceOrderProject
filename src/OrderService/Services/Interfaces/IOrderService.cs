using OrderService.DTOs;
using OrderService.DTOs.OrderDTOs;

namespace OrderService.Services.Interfaces;

public interface IOrderService
{
    Task<ApiResponseDTO<IdDTO>> CreateOrderAsync(CreateOrderDTO createOrderDTO, Guid userId);
    Task<ApiResponseDTO<GetOrderDTO>> GetOrderByIdAsync(Guid orderId);
    Task<ApiResponseDTO<List<GetOrderDTO>>> GetOrdersAsync(Guid? userId, int? pageSize, int? pageNumber);
    Task<ApiResponseDTO<IdDTO>> UpdateOrderAsync(Guid? userId, Guid orderId, UpdateOrderDTO updateOrderDTO);
    Task<ApiResponseDTO<IdDTO>> UpdateOrderStatusAsync(Guid? userId, Guid orderId, Guid statusId);
    Task<ApiResponseNoDataDTO> RemoveOrderAsync(Guid orderId, Guid userId);
}