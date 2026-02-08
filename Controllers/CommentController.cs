using Microsoft.AspNetCore.Mvc;
using stock_fincance_api.Interface;
using stock_fincance_api.Mappers;
using stock_fincance_api.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        return Ok(comments);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
            return NotFound();

        return Ok(comment.ToCommentDto());
    }
}
