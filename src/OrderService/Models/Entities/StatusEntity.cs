namespace OrderService.Models.Entities;

public class StatusEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}