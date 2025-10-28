using OrderService.Models.Context;
using OrderService.Models.Entities;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateOrderAsync(OrderEntity orderEntity)
    {
        await _context.Orders.AddAsync(orderEntity);
        await _context.SaveChangesAsync();
        return orderEntity.Id;
    }
}