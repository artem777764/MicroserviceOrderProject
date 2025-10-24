using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Models;
using UserService.Models.Entities;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expireHours;
    private readonly string _jwtCookieName;

    public JwtService(IOptions<JwtSettings> options)
    {
        _secretKey = options.Value.SecretKey;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expireHours = options.Value.ExpireHours;
        _jwtCookieName = options.Value.JwtCookieName;
    }

    public string GenerateToken(UserEntity user, Guid? activeRoleId = null)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name ?? userRole.Role.Id.ToString()));
            claims.Add(new Claim("role_id", userRole.Role.Id.ToString()));
        }

        if (activeRoleId.HasValue)
        {
            claims.Add(new Claim("active_role", activeRoleId.Value.ToString()));
            string activeRoleName = user.UserRoles.First(r => r.RoleId == activeRoleId.Value).Role.Name;
            if (!string.IsNullOrEmpty(activeRoleName))
                claims.Add(new Claim("active_role_name", activeRoleName));
        }

        SigningCredentials signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256
        );

        JwtSecurityToken Token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_expireHours)
        );
        return new JwtSecurityTokenHandler().WriteToken(Token);
    }

    public int GetExpireHours() => _expireHours;
    public string GetJwtCookieName() => _jwtCookieName;
}