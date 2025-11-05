namespace OrderService.DTOs.OrderDTOs;

public record UpdateOrderDTO
{
    public Guid? StatusId { get; set; }
    public List<CreateOrderItemsDTO>? Items { get; set; }
}