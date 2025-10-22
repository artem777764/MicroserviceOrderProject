using Backend.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO)
    {
        return Ok(await _userService.CreateUserAsync(createUserDTO));
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
    {
        return Ok(await _userService.GetUserByIdAsync(userId));
    }
}