using Microsoft.EntityFrameworkCore;
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

    private IQueryable<OrderEntity> CreateBaseQuery()
    {
        return _context.Orders.Include(o => o.OrderItems)
                              .ThenInclude(oi => oi.Item)
                              .ThenInclude(i => i.Category)
                              .Include(o => o.Status);
    }

    public async Task<OrderEntity?> GetOrderByIdAsync(Guid orderId)
    {
        return await CreateBaseQuery().FirstOrDefaultAsync(o => o.Id == orderId);
    }
}