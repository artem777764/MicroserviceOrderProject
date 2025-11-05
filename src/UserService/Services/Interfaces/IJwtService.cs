using UserService.Models.Entities;

namespace UserService.Services.Interfaces;

public interface IJwtService
{
    string? GenerateToken(UserEntity user, Guid? activeRoleId = null);
    int GetExpireHours();
    string GetJwtCookieName();
}