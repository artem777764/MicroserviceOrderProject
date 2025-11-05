namespace OrderService.DTOs.OrderDTOs;

public record CreateOrderItemsDTO
{
    public required Guid ItemId { get; set; }
    public required int Amount { get; set; }
}