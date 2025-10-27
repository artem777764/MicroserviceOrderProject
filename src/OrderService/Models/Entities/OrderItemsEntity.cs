namespace OrderService.Models.Entities;

public class OrderItemsEntity
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }

    public ItemEntity Item { get; set; } = null!;
    public OrderEntity Order { get; set; } = null!;
}