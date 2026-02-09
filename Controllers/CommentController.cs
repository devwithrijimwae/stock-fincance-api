using Microsoft.AspNetCore.Mvc;
using stock_fincance_api.DTOs.Comments;
using stock_fincance_api.Interface;
using stock_fincance_api.Mappers;
using stock_fincance_api.Models;
using stock_fincance_api.Repositoy;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;

    public CommentsController(ICommentRepository commentRepo, IStockRepository stockRepo)
    {
        _commentRepo= commentRepo;
        _stockRepo= stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepo.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(int id)
    {
        var comment = await _commentRepo.GetByIdAsync(id);
        if (comment == null)
            return NotFound();

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
    {
       
        if (!await _stockRepo.StockExists(stockId))
        {
            return BadRequest("Stock does not exist.");
        }
            

        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetComment),new { id = commentModel.Id},commentModel.ToCommentDto()
        );
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
    {
        var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate(id));
        if (comment == null)
        {
            return NotFound("Comment not found");
        }
        return Ok(comment.ToCommentDto());
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment = await _commentRepo.DeleteAsync(id);
        if (comment == null)
        {
            return NotFound("Comment does not exist");
        }
        return NoContent();
    }

}
