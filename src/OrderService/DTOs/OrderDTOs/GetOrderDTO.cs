namespace OrderService.DTOs.OrderDTOs;

public record GetOrderDTO
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string StatusName { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required List<GetOrderItemsDTO> Items { get; set; }
}