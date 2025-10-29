using Microsoft.EntityFrameworkCore;
using OrderService.Models.Context;
using OrderService.Models.Entities;
using OrderService.Repositories.Interfaces;

namespace UserService.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateItemAsync(ItemEntity itemEntity)
    {
        await _context.Items.AddAsync(itemEntity);
        await _context.SaveChangesAsync();
        return itemEntity.Id;
    }

    public async Task<ItemEntity?> GetByIdAsync(Guid itemId)
    {
        return await BuildDefectsQuery().FirstOrDefaultAsync(i => i.Id == itemId);
    }

    public async Task<List<ItemEntity>> GetAllAsync()
    {
        return await BuildDefectsQuery().ToListAsync();
    }

    private IQueryable<ItemEntity> BuildDefectsQuery()
    {
        return _context.Items.Include(i => i.Category)
                             .AsNoTracking();
    }

    public async Task RemoveByIdAsync(Guid itemId)
    {
        ItemEntity? itemEntity = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
        if (itemEntity != null) _context.Items.Remove(itemEntity);
        await _context.SaveChangesAsync();
    }
}