namespace OrderService.Models.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public Guid StatusId { get; set; }
    public required DateTime CreatedAt { get; set; }

    public StatusEntity Status { get; set; } = null!;
    public List<OrderItemsEntity> OrderItems { get; set; } = new List<OrderItemsEntity>();
}