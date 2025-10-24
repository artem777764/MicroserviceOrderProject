using Microsoft.Extensions.Options;
using UserService.Models;

namespace Backend.Services;

public class JwtCookieService
{
    private readonly int _expireHours;
    private readonly string _jwtCookieName;
    
    public JwtCookieService(IOptions<JwtSettings> options)
    {
        _expireHours = options.Value.ExpireHours;
        _jwtCookieName = options.Value.JwtCookieName;
    }

    public CookieOptions GetAuthCookieOptions(bool expired = false)
    {
        DateTimeOffset expires = expired
            ? DateTimeOffset.UtcNow.AddDays(-1)
            : DateTimeOffset.UtcNow.AddHours(_expireHours);

        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Path = "/",
            Expires = expires,
        };
    }

    public string CookieName => _jwtCookieName;
}