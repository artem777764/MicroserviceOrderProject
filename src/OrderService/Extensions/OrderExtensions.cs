using OrderService.DTOs.OrderDTOs;
using OrderService.Models.Entities;

namespace OrderService.Extensions;

public static class OrderExtensions
{
    public static OrderEntity ToEntity(this CreateOrderDTO createOrderDTO, Guid userId)
    {
        return new OrderEntity()
        {
            UserId = userId,
            StatusId = Guid.Parse("369ab909-6709-4fe2-9e63-5a45d20893be"),
            CreatedAt = DateTime.UtcNow,
            OrderItems = createOrderDTO.Items.Select(i => i.ToEntity()).ToList(),
        };
    }

    public static OrderItemsEntity ToEntity(this CreateOrderItemsDTO createOrderItemsDTO)
    {
        return new OrderItemsEntity()
        {
            ItemId = createOrderItemsDTO.ItemId,
            Amount = createOrderItemsDTO.Amount,
        };
    }
}