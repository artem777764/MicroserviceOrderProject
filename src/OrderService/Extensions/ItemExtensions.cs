using OrderService.DTOs.ItemDTOs;
using OrderService.Models.Entities;

namespace OrderService.Extensions;

public static class ItemExtensions
{
    public static ItemEntity ToEntity(this CreateItemDTO createItemDTO)
    {
        return new ItemEntity()
        {
            Name = createItemDTO.Name,
            CategoryId = createItemDTO.CategoryId,
        };
    }

    public static GetItemDTO ToDTO(this ItemEntity itemEntity)
    {
        return new GetItemDTO()
        {
            Id = itemEntity.Id,
            Name = itemEntity.Name,
            CategoryName = itemEntity.Category.Name,
        };
    }
}