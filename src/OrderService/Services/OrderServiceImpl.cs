using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
}