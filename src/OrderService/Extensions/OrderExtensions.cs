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

    public static OrderEntity UpdateWith(this OrderEntity oldOrderEntity, UpdateOrderDTO newOrderEntity)
    {
        if (newOrderEntity.Items != null) oldOrderEntity.OrderItems = newOrderEntity.Items.Select(oe => oe.ToEntity()).ToList();
        if (newOrderEntity.StatusId != null) oldOrderEntity.StatusId = newOrderEntity.StatusId.GetValueOrDefault();
        return oldOrderEntity;
    }

    public static OrderEntity UpdateStatusIdWith(this OrderEntity orderEntity, Guid stutusId)
    {
        orderEntity.StatusId = stutusId;
        return orderEntity;
    }

    public static OrderItemsEntity ToEntity(this UpdateOrderItemsDTO updateOrderItemsDTO)
    {
        return new OrderItemsEntity()
        {
            ItemId = updateOrderItemsDTO.ItemId,
            Amount = updateOrderItemsDTO.Amount,
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
            Amount = orderItemsEntity.Amount,
            CategoryName = orderItemsEntity.Item.Category.Name,
        };
    }
}