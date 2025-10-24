using Backend.DTOs.UserDTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserService.DTOs;
using UserService.DTOs.UserDataDTOs;
using UserService.DTOs.UserDTOs;
using UserService.Models;
using UserService.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtCookieService _jwtCookieService;

    public UsersController(IUserService userService, JwtCookieService jwtCookieService)
    {
        _userService = userService;
        _jwtCookieService = jwtCookieService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO)
    {
        return Ok(await _userService.CreateUserAsync(createUserDTO));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginDTO loginDTO)
    {
        ApiResponseDTO<GetLoginUserDTO> apiResponseDTO = await _userService.LoginUserAsync(loginDTO);
        if (!apiResponseDTO.Success) return Ok(apiResponseDTO);

        Response.Cookies.Append(
            apiResponseDTO.Data!.JwtCookieName!,
            apiResponseDTO.Data!.JwtToken!,
            _jwtCookieService.GetAuthCookieOptions());

        return Ok(apiResponseDTO);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
    {
        return Ok(await _userService.GetUserByIdAsync(userId));
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUsersAsync()
    {
        return Ok(await _userService.GetAllAsync());
    }
}