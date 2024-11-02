using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Commands.CancelOrder
{
	public class CancelOrderCommandHandler(ILogger<CancelOrderCommandHandler> logger,
		IOrderRepository orderRepository) : IRequestHandler<CancelOrderCommand>
	{
		public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting order with id: {OrderId}", request.OrderId);
			var order = await orderRepository.GetOrderById(request.OrderId);
			if (order != null)
			{
				await orderRepository.DeleteOrderAsync(order);
			}
		}
	}
}
