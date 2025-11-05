namespace OrderService.DTOs.ItemDTOs;

public record GetItemDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CategoryName { get; set; }
}