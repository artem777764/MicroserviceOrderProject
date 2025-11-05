namespace OrderService.Models.Entities;

public class ItemEntity
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public required string Name { get; set; }

    public CategoryEntity Category { get; set; } = null!;
    public List<OrderItemsEntity> OrderItems { get; set; } = new List<OrderItemsEntity>();
}