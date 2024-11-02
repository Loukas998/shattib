using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Orders.Commands.CancelOrder;
using Template.Application.Orders.Commands.CreateOrder;
using Template.Application.Orders.Commands.UpdateOrderStatus;
using Template.Application.Orders.Dtos;
using Template.Application.Orders.Queries.GetAllOrders;
using Template.Application.Orders.Queries.GetByKind;
using Template.Application.Orders.Queries.GetOrderById;
using Template.Application.Orders.Queries.GetOrdersByStatus;
using Template.Application.Orders.Queries.GetUserOrders;
using Template.Application.Orders.Queries.GetUserOrdersByKind;
using Template.Application.Orders.Queries.GetUserOrdersByStatus;

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
			return CreatedAtAction(nameof(GetOrderById), new { orderId = id }, null);
		}

		[HttpGet]
		[Route("{orderId}")]
		public async Task<ActionResult<OrderDto>> GetOrderById([FromRoute] int orderId)
		{
			var order = await mediator.Send(new GetOrderByIdQuery(orderId));
			return Ok(order);
		}

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
		{
			var orders = await mediator.Send(new GetAllOrdersQuery());
			return Ok(orders);
		}

		[HttpGet("kind")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByKind([FromQuery]string kind)
		{
			var orders = await mediator.Send(new GetOrdersByKindQuery(kind));
			return Ok(orders);
		}

		[HttpGet("user/kind")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrdersByKind([FromQuery]string kind)
		{
			var orders = await mediator.Send(new GetUserOrdersByKindQuery(kind));
			return Ok(orders);
		}

		[HttpGet("user")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders()
		{
			var orders = await mediator.Send(new GetUserOrdersQuery());
			return Ok(orders);
		}

		[HttpGet("user/status")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrdersByStatus([FromQuery]string status)
		{
			var orders = await mediator.Send(new GetUserOrdersByStatusQuery(status));
			return Ok(orders);
		}

		[HttpGet("status")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByStatus([FromQuery] string status)
		{
			var orders = await mediator.Send(new GetOrdersByStatusQuery(status));
			return Ok(orders);
		}

		[HttpDelete]
		[Route("{orderId}")]
		public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
		{
			await mediator.Send(new CancelOrderCommand(orderId));
			return NoContent();
		}

		[HttpPatch]
		[Route("{orderId}")]
		public async Task<IActionResult> ChangeStatus([FromRoute]int orderId, [FromBody] UpdateOrderStatusCommand command)
		{
			command.OrderId = orderId;
			await mediator.Send(command);
			return NoContent();
		}
	}
}
