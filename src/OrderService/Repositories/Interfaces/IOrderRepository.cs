using OrderService.Models.Entities;

namespace OrderService.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Guid> CreateOrderAsync(OrderEntity orderEntity);
}