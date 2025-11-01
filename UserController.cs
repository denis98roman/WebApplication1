using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoogleJwtApp.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var picture = User.FindFirst("picture")?.Value ?? "";

        return Ok(new { email, name, picture });
    }
}
