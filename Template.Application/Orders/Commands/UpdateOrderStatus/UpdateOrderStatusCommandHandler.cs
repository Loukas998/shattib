using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Orders;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Commands.UpdateOrderStatus
{
	public class UpdateOrderStatusCommandHandler(ILogger<UpdateOrderStatusCommandHandler> logger,
		IOrderRepository orderRepository) : IRequestHandler<UpdateOrderStatusCommand>
	{
		public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Changing the stauts of order with id: {OrderId} to {NewStatus}", request.OrderId, request.NewStatus);
			var order = await orderRepository.GetOrderById(request.OrderId);
            if (order == null)
            {
				throw new NotFoundException(nameof(Order), request.OrderId.ToString());
			}
			order.Status = request.NewStatus;
			await orderRepository.SaveChangesAsync();
		}
	}
}
