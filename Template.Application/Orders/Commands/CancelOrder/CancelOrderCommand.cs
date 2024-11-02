using MediatR;

namespace Template.Application.Orders.Commands.CancelOrder
{
	public class CancelOrderCommand(int orderId) : IRequest
	{
		public int OrderId { get; } = orderId;
	}
}
