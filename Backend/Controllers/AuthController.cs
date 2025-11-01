using JwtCommentsApp.Models;
using JwtCommentsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtCommentsApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private static List<User> users = new();
    private readonly JwtService _jwt = new();

    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        users.Add(user);
        return Ok("Registered");
    }

    [HttpPost("login")]
    public IActionResult Login(User login)
    {
        var user = users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
        if (user == null) return Unauthorized();
        var token = _jwt.GenerateToken(user.Username, user.Role);
        return Ok(new { token });
    }
}
