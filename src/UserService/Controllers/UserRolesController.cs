using Microsoft.AspNetCore.Mvc;
using UserService.DTOs.UserRoleDTOs;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("user-roles")]
public class UserRolesController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRolesController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    [HttpPost("appoint")]
    public async Task<IActionResult> AppointRoleAsync([FromBody] CreateUserRoleDTO createUserRoleDTO)
    {
        await _userRoleService.AppointRoleAsync(createUserRoleDTO);
        return Ok();
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveRoleAsync([FromBody] RemoveUserRoleDTO removeUserRoleDTO)
    {
        await _userRoleService.RemoveRoleAsync(removeUserRoleDTO);
        return Ok();
    }
}