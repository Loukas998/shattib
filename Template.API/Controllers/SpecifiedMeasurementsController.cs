using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.SpecifiedMeasurements.Commands.CreateSpecifiedMeasurements;
using Template.Application.SpecifiedMeasurements.Commands.DeleteSpecifiedMeasurements;
using Template.Application.SpecifiedMeasurements.Commands.UpdateSpecifiedMeasurements;
using Template.Application.SpecifiedMeasurements.Dtos;
using Template.Application.SpecifiedMeasurements.Queries.GetAll;
using Template.Application.SpecifiedMeasurements.Queries.GetById;
using Template.Application.SpecifiedMeasurements.Queries.GetByUser;
using Template.Domain.Constants;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = $"{UserRoles.Administrator},{UserRoles.Business},{UserRoles.Client}")]
	public class SpecifiedMeasurementsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateSpecifiedMeasurements([FromForm] CreateSpecifiedMeasurementsCommand command)
		{
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetSpecifiedMeasurementById), new { id = id }, null);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SpecifiedMeasurementDto>> GetSpecifiedMeasurementById([FromRoute] int id)
		{
			var specM = await mediator.Send(new GetSpecifiedMeasurementByIdQuery(id));
			return Ok(specM);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<SpecifiedMeasurementDto>>> GetAllSpecifiedMeasurement()
		{
			var specMs = await mediator.Send(new GetAllSpecifiedMeasurementsQuery());
			return Ok(specMs);
		}

		[HttpGet("Mine")]
		public async Task<ActionResult<IEnumerable<SpecifiedMeasurementDto>>> GetUserSpecifiedMeasurement()
		{
			var specMs = await mediator.Send(new GetUserSpecifiedMeasurementsQuery());
			return Ok(specMs);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSpecifiedMeasurement([FromRoute] int id)
		{
			var command = new DeleteSpecifiedMeasurementsCommand
			{
				Id = id
			};
			await mediator.Send(command);
			return NoContent();
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateSpecifiedMeasurement(
			[FromBody] UpdateSpecifiedMeasurementsCommand command, [FromRoute] int id)
		{
			command.Id = id;
			await mediator.Send(command);
			return NoContent();
		}
	}
}
