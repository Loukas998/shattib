using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.CriteriaBills.Commands.AddReceiptCommand;
using Template.Application.CriteriaBills.Commands.CreateCriteriaBillCommand;
using Template.Application.CriteriaBills.Commands.UpdateCriteriaBillAcceptedCommand;
using Template.Application.CriteriaBills.Queries.GetCriteriaBillByIdQuery;
using Template.Domain.Constants;

namespace Template.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.Business}")]
public class CriteriaBillsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = UserRoles.Administrator)]
    public async Task<IActionResult> CreateBill([FromForm] CreateCriteriaBillCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetBillById), new { id }, null);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBillById(int id)
    {
        var bill = await mediator.Send(new GetCriteriaBillByIdQuery(id));
        return Ok(bill);
    }

    [HttpPatch("{id:int}/Accepted")]
    [Authorize(Roles = UserRoles.Business)]
    public async Task<IActionResult> UpdateAcceptedBill([FromRoute] int id,
        [FromBody] UpdateCriteriaBillAcceptedCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id:int}/Receipt")]
    [Authorize(Roles = UserRoles.Business)]
    public async Task<IActionResult> AddReceipt(int id, [FromForm] AddReceiptCommand command)
    {
        command.Id = id;
        var bill = await mediator.Send(command);
        return Ok(bill);
    }
}