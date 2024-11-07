using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Comments.Commands.CreateCommentCommand;
using Template.Application.Comments.Dtos;
using Template.Application.Comments.Queries.GetAllForCriteria;
using Template.Application.Comments.Queries.GetComment;

namespace Template.API.Controllers;

[ApiController]
[Route("api/Criterias/{criteriaId:int}/[controller]")]
public class CommentsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateComment([FromRoute] int criteriaId, [FromForm] CreateCommentCommand command)
    {
        command.CriteriaId = criteriaId;
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCommentById), new { id }, null);
    }

    // Note:: Added the absolute path to prevent having to add CriteriaId
    //          because it's not needed for this route
    [HttpGet("/api/Comments/{id:int}")]
    public async Task<ActionResult<CommentDto>> GetCommentById([FromRoute] int id)
    {
        var comment = await mediator.Send(new GetCommentByIdQuery(id));
        return Ok(comment);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllForCriteria([FromRoute] int criteriaId,
        [FromQuery] int lastCommentId)
    {
        var query = new GetAllCommentsForCriteriaQuery
        {
            LastCommentId = lastCommentId,
            CriteriaId = criteriaId
        };
        var comments = await mediator.Send(query);
        return Ok(comments);
    }
}