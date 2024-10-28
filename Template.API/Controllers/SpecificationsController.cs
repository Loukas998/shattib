using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Specifications.Commands.CreateCommand;
using Template.Application.Specifications.Commands.UpdateCommand;
using Template.Application.Specifications.Queries.GetAllSpecifications;
using Template.Application.Specifications.Queries.GetSpecificationById;
using Template.Domain.Entities.Products;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/Products/[controller]")]
	public class SpecificationsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> AddSpecification([FromBody]CreateSpecificationCommand command)
		{
			int id = await mediator.Send(command);
			return Ok(id);
		}

		[HttpPatch]
		[Route("{specificationId}")]
		public async Task<IActionResult> AddSpecification([FromBody] UpdateSpecificationCommand command,
			[FromRoute]int specificationId)
		{
			command.SpecificationId = specificationId;
			await mediator.Send(command);
			return NoContent();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Specification>>> GetAllSpecifications()
		{
			return Ok(await mediator.Send(new GetAllSpecificationsQuery()));
		}

		[HttpGet]
		[Route("{specificationId}")]
		public async Task<ActionResult<Specification>> GetAllSpecifications([FromRoute]int specificationId)
		{
			return Ok(await mediator.Send(new GetSpecificationByIdQuery(specificationId)));
		}
	}
}
