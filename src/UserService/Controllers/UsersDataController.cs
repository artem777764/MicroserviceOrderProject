using Backend.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs.UserDataDTOs;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("users-data")]
public class UsersDataController : ControllerBase
{
    private readonly IUserDataService _userDataService;

    public UsersDataController(IUserDataService userDataService)
    {
        _userDataService = userDataService;
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateUserDataAsync([FromBody] CreateUserDataDTO createUserDataDTO, [FromRoute] Guid userId)
    {
        // ДОБАВИТЬ ID ИЗ JWT
        return Ok(await _userDataService.CreateUserDataAsync(userId, createUserDataDTO));
    }
}