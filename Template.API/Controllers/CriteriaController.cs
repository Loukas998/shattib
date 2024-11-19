using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Criterias.Commands.CreateCriteriaCommand;
using Template.Application.Criterias.Commands.DeleteCriteria;
using Template.Application.Criterias.Commands.UpdateCriteriaStatusCommand;
using Template.Application.Criterias.Dtos;
using Template.Application.Criterias.Queries.GetAllCriterias;
using Template.Application.Criterias.Queries.GetCriteriaByIdQuery;
using Template.Application.Criterias.Queries.GetCriteriasForUserQuery;
using Template.Domain.Constants;

namespace Template.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.Business}")]
public class CriteriaController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCriteria([FromForm] CreateCriteriaCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCriteriaById), new { id }, null);
    }

    [HttpGet("{id:int}")]
    //[Authorize(Roles = $"{UserRoles.Business}")]
    public async Task<IActionResult> GetCriteriaById(int id)
    {
        var criteria = await mediator.Send(new GetCriteriaByIdQuery(id));
        return Ok(criteria);
    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetCriteriasForUser([FromQuery] string? status)
    {
        var criterias = await mediator.Send(new GetCriteriasForUserQuery(status));
        return Ok(criterias);
    }


    [HttpGet("GetAll")]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<ActionResult<IEnumerable<CriteriaDto>>> GetAllCriterias([FromQuery] string? status)
    {
        var criterias = await mediator.Send(new GetAllCriteriasQuery(status));
        return Ok(criterias);
    }


    [HttpPatch("{id:int}/Status")]
    public async Task<IActionResult> UpdateCriteriaStatus(int id, [FromBody] UpdateCriteriaStatusCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = UserRoles.Administrator)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCriteria(int id)
    {
        var command = new DeleteCriteriaCommand(id);
        await mediator.Send(command);
        return NoContent();
    }
}