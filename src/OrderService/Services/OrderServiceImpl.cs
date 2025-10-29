using OrderService.DTOs;
using OrderService.DTOs.OrderDTOs;
using OrderService.Extensions;
using OrderService.Models;
using OrderService.Models.Entities;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class OrderServiceImpl : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderServiceImpl(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ApiResponseDTO<IdDTO>> CreateOrderAsync(CreateOrderDTO createOrderDTO, Guid userId)
    {
        ApiResponseDTOBuilder<IdDTO> apiResponseDTOBuilder = new ApiResponseDTOBuilder<IdDTO>();

        Guid orderId = await _orderRepository.CreateOrderAsync(createOrderDTO.ToEntity(userId));
        IdDTO idDTO = new IdDTO() { Id = orderId };

        return apiResponseDTOBuilder.SetData(idDTO)
                                    .SetSuccessful()
                                    .Build();
    }

    public async Task<ApiResponseDTO<GetOrderDTO>> GetOrderByIdAsync(Guid orderId)
    {
        ApiResponseDTOBuilder<GetOrderDTO> apiResponseDTOBuilder = new ApiResponseDTOBuilder<GetOrderDTO>();

        OrderEntity? orderEntity = await _orderRepository.GetOrderByIdAsync(orderId);
        if (orderEntity == null)
        {
            return apiResponseDTOBuilder.SetError(ResponseErrors.OrderNotFound())
                                        .Build();
        }

        return apiResponseDTOBuilder.SetData(orderEntity.ToDTO())
                                    .SetSuccessful()
                                    .Build();
    }

    public async Task<ApiResponseDTO<List<GetOrderDTO>>> GetOrdersAsync(Guid? userId, int? pageSize, int? pageNumber)
    {
        ApiResponseDTOBuilder<List<GetOrderDTO>> apiResponseDTOBuilder = new ApiResponseDTOBuilder<List<GetOrderDTO>>();

        List<OrderEntity> orderEntities = await _orderRepository.GetOrdersAsync(userId, pageSize, pageNumber);
        return apiResponseDTOBuilder.SetData(orderEntities.Select(oe => oe.ToDTO()).ToList())
                                    .SetSuccessful()
                                    .Build();
    }

    public async Task<ApiResponseDTO<IdDTO>> UpdateOrderAsync(Guid? userId, Guid orderId, UpdateOrderDTO updateOrderDTO)
    {
        ApiResponseDTOBuilder<IdDTO> apiResponseDTOBuilder = new ApiResponseDTOBuilder<IdDTO>();

        OrderEntity? oldOrderEntity = await _orderRepository.GetOrderByIdAsync(orderId);
        if (oldOrderEntity == null)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.OrderNotFound());
            return apiResponseDTOBuilder.Build();
        }

        if (oldOrderEntity.UserId != userId)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.Forbidden());
            return apiResponseDTOBuilder.Build();
        }

        Guid? foundOrderId = await _orderRepository.UpdateOrderAsync(oldOrderEntity.UpdateWith(updateOrderDTO));
        if (foundOrderId == null)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.OrderNotFound());
            return apiResponseDTOBuilder.Build();
        }

        IdDTO idDTO = new IdDTO { Id = foundOrderId.GetValueOrDefault() };
        return apiResponseDTOBuilder.SetData(idDTO)
                                    .SetSuccessful()
                                    .Build();
    }
    
    public async Task<ApiResponseDTO<IdDTO>> UpdateOrderStatusAsync(Guid? userId, Guid orderId, Guid statusId)
    {
        ApiResponseDTOBuilder<IdDTO> apiResponseDTOBuilder = new ApiResponseDTOBuilder<IdDTO>();

        OrderEntity? oldOrderEntity = await _orderRepository.GetOrderByIdAsync(orderId);
        if (oldOrderEntity == null)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.OrderNotFound());
            return apiResponseDTOBuilder.Build();
        }

        if (oldOrderEntity.UserId != userId)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.Forbidden());
            return apiResponseDTOBuilder.Build();
        }

        Guid? foundOrderId = await _orderRepository.UpdateOrderAsync(oldOrderEntity.UpdateStatusIdWith(statusId));
        if (foundOrderId == null)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.OrderNotFound());
            return apiResponseDTOBuilder.Build();
        }

        IdDTO idDTO = new IdDTO { Id = foundOrderId.GetValueOrDefault() };
        return apiResponseDTOBuilder.SetData(idDTO)
                                    .SetSuccessful()
                                    .Build();
    }

    public async Task<ApiResponseNoDataDTO> RemoveOrderAsync(Guid orderId, Guid userId)
    {
        ApiResponseNoDataDTOBuilder apiResponseNoDataDTOBuilder = new ApiResponseNoDataDTOBuilder();

        OrderEntity? orderEntity = await _orderRepository.GetOrderByIdAsync(orderId);
        if (orderEntity == null)
        {
            apiResponseNoDataDTOBuilder.SetError(ResponseErrors.OrderNotFound());
            return apiResponseNoDataDTOBuilder.Build();
        }

        if (orderEntity.UserId != userId)
        {
            apiResponseNoDataDTOBuilder.SetError(ResponseErrors.Forbidden());
            return apiResponseNoDataDTOBuilder.Build();
        }

        await _orderRepository.RemoveOrderAsync(orderEntity);
        return apiResponseNoDataDTOBuilder.SetSuccessful()
                                          .Build();
    }
}