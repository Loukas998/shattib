using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.ContactUs.Command.SendContactUs;
using Template.Application.ContactUs.Queries.GetAll;
using Template.Application.ContactUs.Queries.GetById;
using Template.Domain.Entities;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ContactUsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> SendContactUs([FromBody] SendContactUsCommand command)
		{
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetContactUsById), new { id }, null);
		}

		[HttpGet]
		public async Task<ActionResult<ContactUs>> GetContactUsById([FromRoute] int id)
		{
			var contactForm = await mediator.Send(new GetByIdQuery(id));
			return Ok(contactForm);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ContactUs>>> GetAllContacts()
		{
			var contactForms = await mediator.Send(new GetAllQuery());
			return Ok(contactForms);
		}
	}
}
