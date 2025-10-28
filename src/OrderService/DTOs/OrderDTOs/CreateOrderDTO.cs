namespace OrderService.DTOs.OrderDTOs;

public record CreateOrderDTO
{
    public required List<CreateOrderItemsDTO> Items { get; set; }
}