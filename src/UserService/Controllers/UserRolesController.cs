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
    [GatewayAuthorizeByRoles("Admin")]
    public async Task<IActionResult> AppointRoleAsync([FromBody] CreateUserRoleDTO createUserRoleDTO)
    {
        return Ok(await _userRoleService.AppointRoleAsync(createUserRoleDTO));
    }

    [HttpDelete("remove")]
    [GatewayAuthorizeByRoles("Admin")]
    public async Task<IActionResult> RemoveRoleAsync([FromBody] RemoveUserRoleDTO removeUserRoleDTO)
    {
        return Ok(await _userRoleService.RemoveRoleAsync(removeUserRoleDTO));
    }
}