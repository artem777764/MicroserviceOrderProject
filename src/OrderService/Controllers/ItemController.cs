using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.DTOs.ItemDTOs;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpPost("")]
    [GatewayAuthorizeByRoles("Manager", "Admin")]
    public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemDTO createItemDTO)
    {
        ApiResponseDTO<IdDTO> apiResponseDTO = await _itemService.CreateUserAsync(createItemDTO);
        return Ok(apiResponseDTO);
    }

    [HttpGet("{itemId}")]
    [GatewayAuthorize]
    public async Task<IActionResult> GetItemByIdAsync([FromRoute] Guid itemId)
    {
        ApiResponseDTO<GetItemDTO> apiResponseDTOs = await _itemService.GetItemByIdAsync(itemId);
        return Ok(apiResponseDTOs);
    }

    [HttpGet("")]
    [GatewayAuthorize]
    public async Task<IActionResult> GetItemsAsync()
    {
        ApiResponseDTO<List<GetItemDTO>> apiResponseDTOs = await _itemService.GetAllAsync();
        return Ok(apiResponseDTOs);
    }

    [HttpDelete("{itemId}")]
    [GatewayAuthorizeByRoles("Manager", "Admin")]
    public async Task<IActionResult> RemoveUserAsync([FromRoute] Guid itemId)
    {
        ApiResponseNoDataDTO apiResponseDTO = await _itemService.RemoveByIdAsync(itemId);
        return Ok(apiResponseDTO);
    }
}