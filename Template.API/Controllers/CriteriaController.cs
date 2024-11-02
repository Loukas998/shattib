using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Criterias.Commands.CreateCriteriaCommand;

namespace Template.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CriteriaController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCriteria([FromForm] CreateCriteriaCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetCriteriaById), new { id }, null);
    }

    [HttpGet("{id:int}")]
    public Task<IActionResult> GetCriteriaById(int id)
    {
        throw new NotImplementedException();
    }
}