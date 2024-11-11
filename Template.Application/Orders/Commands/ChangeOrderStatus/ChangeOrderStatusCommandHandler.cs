using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Commands.ChangeOrderStatus
{
	public class ChangeOrderStatusCommandHandler(ILogger<ChangeOrderStatusCommandHandler> logger,
		IOrderRepository orderRepository) : IRequestHandler<ChangeOrderStatusCommand>
	{
		public async Task Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("changing order status with id: {OrderId} to status: {NewStatus}",
				request.OrderId, request.NewStatus);

			var order = await orderRepository.GetOrderById(request.OrderId);
			if (order == null)
			{
				throw new NotFoundException(nameof(order), request.OrderId.ToString());
			}
			order.Status = request.NewStatus;
			if(request.DateOfArrival != null)
			{
				order.DateOfArrival = request.DateOfArrival;
			}
			await orderRepository.SaveChangesAsync();
		}
	}
}
