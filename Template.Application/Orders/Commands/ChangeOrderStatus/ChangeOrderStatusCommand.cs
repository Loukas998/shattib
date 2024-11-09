using MediatR;

namespace Template.Application.Orders.Commands.ChangeOrderStatus
{
	public class ChangeOrderStatusCommand : IRequest
	{
		public int OrderId { get; set; }
		public string NewStatus { get; set; } = default!;
	}
}
