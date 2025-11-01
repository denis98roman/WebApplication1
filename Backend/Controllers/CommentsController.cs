using JwtCommentsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtCommentsApp.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private static List<Comment> comments = new();
    private static int nextId = 1;

    [HttpGet]
    public IActionResult GetAll() => Ok(comments);

    [Authorize]
    [HttpPost]
    public IActionResult Add(Comment comment)
    {
        var username = User.Identity?.Name;
        comment.Id = nextId++;
        comment.Author = username!;
        comments.Add(comment);
        return Ok(comment);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);
        if (comment == null) return NotFound();

        var username = User.Identity?.Name;
        var isAdmin = User.IsInRole("Admin");

        if (comment.Author == username || isAdmin)
        {
            comments.Remove(comment);
            return Ok("Deleted");
        }

        return Forbid();
    }
}
