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
    public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemDTO createItemDTO)
    {
        ApiResponseDTO<IdDTO> apiResponseDTO = await _itemService.CreateUserAsync(createItemDTO);
        return Ok(apiResponseDTO);
    }
}