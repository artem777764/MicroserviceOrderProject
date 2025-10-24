using Microsoft.Extensions.Options;
using UserService.Models;

namespace Backend.Services;

public class JwtCookieService
{
    private readonly int _expireHours;
    public JwtCookieService(IOptions<JwtSettings> options)
    {
        _expireHours = options.Value.ExpireHours;
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
}