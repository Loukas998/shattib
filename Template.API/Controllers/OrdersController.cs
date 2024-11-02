using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Orders.Commands;

namespace Template.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
		{
			int id = await mediator.Send(command);
			return Ok(id);
		}
	}
}
