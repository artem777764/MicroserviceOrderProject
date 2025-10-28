namespace OrderService.DTOs.OrderDTOs;

public record GetOrderItemsDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CategoryName { get; set; }
}