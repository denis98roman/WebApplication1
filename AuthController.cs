using GoogleJwtApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoogleJwtApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly GoogleJwtService _googleJwt;
    private readonly JwtService _jwt;

    public AuthController(GoogleJwtService googleJwt, JwtService jwt)
    {
        _googleJwt = googleJwt;
        _jwt = jwt;
    }

    [HttpPost("google-token")]
    public async Task<IActionResult> GoogleToken([FromBody] string idToken)
    {
        var payload = await _googleJwt.GetPayload(idToken);
        if (payload == null)
            return Unauthorized("Invalid Google token");

        var token = _jwt.GenerateToken(payload.Email, payload.Name, payload.Picture ?? "");
        return Ok(new { token });
    }
}
