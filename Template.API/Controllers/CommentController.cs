using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Comments.Commands.CreateCommentCommand;
using Template.Application.Comments.Queries.GetComment;
using Template.Domain.Entities.Criteria;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateComment([FromForm] CreateCommentCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCommentById), new { id }, null);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Comment>> GetCommentById([FromRoute] int id)
    {
        var comment = await mediator.Send(new GetCommentByIdQuery(id));
        return Ok(comment);
    }
}