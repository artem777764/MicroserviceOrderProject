using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("headers")]
    public class HeadersController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetAuthorizationHeaders()
        {
            List<string> headerKeys =
            [
                "Gateway-Auth-Valid",
                "Gateway-Auth-Reason",
                "Gateway-User-Id",
                "Gateway-Auth-Jti",
                "Gateway-User-Role-Ids",
                "Gateway-User-Role-Names",
                "Gateway-Active-Role-Id",
                "Gateway-Active-Role-Name",
            ];

            Dictionary<string, string?> headersDictionary = headerKeys.ToDictionary(
                key => key,
                key => Request.Headers.TryGetValue(key, out var value) ? value.ToString() : null
            );

            return Ok(headersDictionary);
        }
    }
}
