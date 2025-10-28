using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.DTOs.OrderDTOs;
using OrderService.Extensions;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class OrderServiceImpl : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidationService _validationService;

    public OrderServiceImpl(IOrderRepository orderRepository, IValidationService validationService)
    {
        _orderRepository = orderRepository;
        _validationService = validationService;
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
}