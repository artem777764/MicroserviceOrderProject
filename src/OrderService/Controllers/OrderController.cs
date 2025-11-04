using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.DTOs.ItemDTOs;
using OrderService.DTOs.OrderDTOs;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[ApiController]
[Route("orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("")]
    [GatewayAuthorize]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDTO createOrderDTO)
    {
        string userId = HttpContext.Request.Headers["Gateway-User-Id"].FirstOrDefault()!;
        ApiResponseDTO<IdDTO> apiResponseDTO = await _orderService.CreateOrderAsync(createOrderDTO, Guid.Parse(userId));
        return Ok(apiResponseDTO);
    }

    [HttpGet("{orderId}")]
    [GatewayAuthorize]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid orderId)
    {
        return Ok(await _orderService.GetOrderByIdAsync(orderId));
    }

    [HttpGet("")]
    [GatewayAuthorize]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] Guid? userId, [FromQuery] int? pageSize, [FromQuery] int? pageNumber)
    {
        return Ok(await _orderService.GetOrdersAsync(userId, pageSize, pageNumber));
    }

    [HttpPut("{orderId}")]
    [GatewayAuthorize]
    public async Task<IActionResult> UpdateOrderAsync([FromRoute] Guid orderId, [FromBody] UpdateOrderDTO updateOrderDTO)
    {
        string userId = HttpContext.Request.Headers["Gateway-User-Id"].FirstOrDefault()!;
        return Ok(await _orderService.UpdateOrderAsync(Guid.Parse(userId), orderId, updateOrderDTO));
    }

    [HttpPut("{orderId}/status/{statusId}")]
    [GatewayAuthorize]
    public async Task<IActionResult> UpdateOrderAsync([FromRoute] Guid orderId, [FromRoute] Guid statusId)
    {
        string userId = HttpContext.Request.Headers["Gateway-User-Id"].FirstOrDefault()!;
        return Ok(await _orderService.UpdateOrderStatusAsync(Guid.Parse(userId), orderId, statusId));
    }

    [HttpDelete("{orderId}")]
    [GatewayAuthorize]
    public async Task<IActionResult> RemoveOrderAsync([FromRoute] Guid orderId)
    {
        string userId = HttpContext.Request.Headers["Gateway-User-Id"].FirstOrDefault()!;
        return Ok(await _orderService.RemoveOrderAsync(orderId, Guid.Parse(userId)));
    }
}