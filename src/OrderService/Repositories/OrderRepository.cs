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
                              .Include(o => o.Status)
                              .OrderByDescending(o => o.CreatedAt)
                              .AsNoTracking();
    }

    public async Task<OrderEntity?> GetOrderByIdAsync(Guid orderId)
    {
        return await CreateBaseQuery().FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<OrderEntity>> GetOrdersAsync(Guid? userId, int? pageSize, int? pageNumber)
    {
        IQueryable<OrderEntity> query = CreateBaseQuery();
        if (userId != null) query = query.Where(o => o.UserId == userId);
        if (pageSize != null && pageNumber != null)
        {
            int queryPageSize = pageSize.GetValueOrDefault();
            int queryPageNumber = pageNumber.GetValueOrDefault();
            query = query.Skip(queryPageSize * (queryPageNumber - 1)).Take(queryPageSize);
        }

        return await query.ToListAsync();
    }
}