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

    public static GetOrderDTO ToDTO(this OrderEntity orderEntity)
    {
        return new GetOrderDTO()
        {
            Id = orderEntity.Id,
            UserId = orderEntity.UserId,
            StatusName = orderEntity.Status.Name,
            CreatedAt = orderEntity.CreatedAt,
            Items = orderEntity.OrderItems.Select(oi => oi.ToDTO()).ToList(),
        };
    }

    public static GetOrderItemsDTO ToDTO(this OrderItemsEntity orderItemsEntity)
    {
        return new GetOrderItemsDTO()
        {
            Id = orderItemsEntity.Item.Id,
            Name = orderItemsEntity.Item.Name,
            CategoryName = orderItemsEntity.Item.Category.Name,
        };
    }
}