using OrderService.Models.Entities;

namespace OrderService.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Guid> CreateOrderAsync(OrderEntity orderEntity);
    Task<OrderEntity?> GetOrderByIdAsync(Guid orderId);
    Task<List<OrderEntity>> GetOrdersAsync(Guid? userId, int? pageSize, int? pageNumber);
    Task RemoveOrderAsync(OrderEntity orderEntity);
}