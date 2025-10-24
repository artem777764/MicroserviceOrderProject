namespace UserService.DTOs.UserDTOs;

public record GetLoginUserDTO
{
    public Guid? UserId { get; set; }
    public Guid? CurrentRoleId { get; set; }
    public string? JwtToken { get; set; }
    public string? JwtCookieName { get; set; }
}