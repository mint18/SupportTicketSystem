using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.Models;
using TicketSystem.Application.Comments.Commands;
using TicketSystem.Application.Comments.Queries;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api/tickets/{ticketId}/comments")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(int ticketId, [FromBody] CreateCommentDto dto)
    {
        try
        {
            var commentId = await _mediator.Send(new CreateCommentCommand(ticketId, dto.Content));
            return CreatedAtAction(nameof(GetComment), new { ticketId, commentId }, new { id = commentId });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetComments(int ticketId)
    {
        var comments = await _mediator.Send(new GetCommentsQuery(ticketId));
        return Ok(comments);
    }

    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetComment(int ticketId, int commentId)
    {
        var comment = await _mediator.Send(new GetCommentQuery(ticketId, commentId));
        if (comment == null)
            return NotFound();
        return Ok(comment);
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int ticketId, int commentId)
    {
        try
        {
            await _mediator.Send(new DeleteCommentCommand(ticketId, commentId));
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}