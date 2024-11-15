using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Consultations.Commands.ChangeStatus;
using Template.Application.Consultations.Commands.CreateConsultation;
using Template.Application.Consultations.Commands.UpdateConsultation;
using Template.Application.Consultations.Dtos;
using Template.Application.Consultations.Queries.GetAllConsultations;
using Template.Application.Consultations.Queries.GetConsultationById;
using Template.Application.Consultations.Queries.GetUserConsultations;
using Template.Domain.Constants;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	//[Authorize(Roles = UserRoles.Client)]
	//[Authorize(Roles = UserRoles.Administrator)]
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
			var consultation = await mediator.Send(new GetConsultationByIdQuery(consultationId));
			return Ok(consultation);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetAllConsultations()
		{
			var consultations = await mediator.Send(new GetAllConsultationsQuery());
			return Ok(consultations);
		}

		[HttpGet("GetUserConsultations")]
		public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetUserConsultations()
		{
			var userConsultations = await mediator.Send(new GetUserConsultationsQuery());
			return Ok(userConsultations);
		}

		[HttpPatch]
		[Route("ChangeStatus/{consultationId}")]
		public async Task<IActionResult> ChangeConsultationStatus([FromRoute] int consultationId, [FromBody]ChangeStatusCommand command)
		{
			command.ConsultationId = consultationId;
			await mediator.Send(command);
			return NoContent();
		}

		[HttpPatch]
		[Route("{consultationId}")]
		public async Task<IActionResult> UpdateConsultation([FromRoute] int consultationId,
			[FromBody] UpdateConsultationCommand command)
		{
			command.ConsultationId = consultationId;
			await mediator.Send(command);
			return NoContent();
		}
	}
}
