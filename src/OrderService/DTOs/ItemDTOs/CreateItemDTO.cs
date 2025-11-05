namespace OrderService.DTOs.ItemDTOs;

public record CreateItemDTO
{
    public required Guid CategoryId { get; set; }
    public required string Name { get; set; }
}