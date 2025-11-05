namespace OrderService.Models.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public List<ItemEntity> Items { get; set; } = new List<ItemEntity>();
}