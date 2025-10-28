using OrderService.DTOs;
using OrderService.DTOs.OrderDTOs;

namespace OrderService.Services.Interfaces;

public interface IOrderService
{
    Task<ApiResponseDTO<IdDTO>> CreateOrderAsync(CreateOrderDTO createOrderDTO, Guid userId);
    Task<ApiResponseDTO<GetOrderDTO>> GetOrderByIdAsync(Guid orderId);
}