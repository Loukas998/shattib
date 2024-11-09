using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Consultations.Commands.ChangeStatus;
using Template.Application.Consultations.Commands.CreateConsultation;
using Template.Application.Consultations.Dtos;
using Template.Application.Consultations.Queries.GetAllConsultations;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ConsultationsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateConsultationAsync([FromBody] CreateConsultationCommand command)
		{
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetConsultationById), new { consultationId = id }, null);
		}

		[HttpGet]
		[Route("{consultationId}")]
		public async Task<ActionResult<ConsultationDto>> GetConsultationById([FromRoute] int consultationId)
		{
			var consultation = await mediator.Send(GetConsultationById(consultationId));
			return Ok(consultation);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetAllConsultations()
		{
			var consultations = await mediator.Send(new GetAllConsultationsQuery());
			return Ok(consultations);
		}

		[HttpPatch]
		[Route("{consultationId}")]
		public async Task<ActionResult<ConsultationDto>> ChangeConsultationStatus([FromRoute] int consultationId, ChangeStatusCommand command)
		{
			await mediator.Send(new ChangeStatusCommand(consultationId));
			return NoContent();
		}
	}
}
