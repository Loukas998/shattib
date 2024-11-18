using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.CriteriaBills.Commands.AddReceiptCommand;
using Template.Application.CriteriaBills.Commands.CreateCriteriaBillCommand;
using Template.Application.CriteriaBills.Commands.UpdateCriteriaBillAcceptedCommand;
using Template.Application.CriteriaBills.Queries.GetCriteriaBillByIdQuery;

namespace Template.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CriteriaBillsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
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
    public async Task<IActionResult> UpdateAcceptedBill(int id, [FromBody] UpdateCriteriaBillAcceptedCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id:int}/Receipt")]
    public async Task<IActionResult> AddReceipt(int id, [FromForm] AddReceiptCommand command)
    {
        command.Id = id;
        var bill = await mediator.Send(command);
        return Ok(bill);
    }
}