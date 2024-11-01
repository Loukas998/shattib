using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Commands
{
	public class CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, 
		IOrderRepository orderRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<CreateOrderCommand, int>
	{
		public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating order: {@Order}", request);
			var order = mapper.Map<Order>(request);
			order.UserId = userContext.GetCurrentUser().Id;
			return await orderRepository.CreateOrderAsync(order);
		}
	}
}
