using OrderService.Models.Entities;

namespace OrderService.Repositories.Interfaces;

public interface IItemRepository
{
    Task<Guid> CreateItemAsync(ItemEntity itemEntity);
    Task<List<ItemEntity>> GetAllAsync();
    Task<ItemEntity?> GetByIdAsync(Guid itemId);
    Task RemoveByIdAsync(Guid itemId);
}