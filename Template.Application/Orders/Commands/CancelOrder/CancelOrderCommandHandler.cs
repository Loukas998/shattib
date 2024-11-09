using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Orders;
using Template.Domain.Exceptions;
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
			if (order == null)
			{
				throw new NotFoundException(nameof(Order), request.OrderId.ToString());
			}
			await orderRepository.DeleteOrderAsync(order);
		}
	}
}
