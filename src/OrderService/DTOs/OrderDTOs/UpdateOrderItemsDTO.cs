namespace OrderService.DTOs.OrderDTOs;

public record UpdateOrderItemsDTO
{
    public required Guid ItemId { get; set; }
    public required int Amount { get; set; }
}