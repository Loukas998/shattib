using MediatR;

namespace Template.Application.Orders.Commands.UpdateOrderStatus
{
	public class UpdateOrderStatusCommand : IRequest
	{
		public int OrderId { get; set; }
		public string NewStatus { get; set; } = default!;
	}
}
