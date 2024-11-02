using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetUserOrdersByKind
{
	public class GetUserOrdersByKindQueryHandler(ILogger<GetUserOrdersByKindQueryHandler> logger,
		IMapper mapper, IOrderRepository orderRepository, IUserContext userContext) : IRequestHandler<GetUserOrdersByKindQuery, IEnumerable<OrderDto>>
	{
		public async Task<IEnumerable<OrderDto>> Handle(GetUserOrdersByKindQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting user orders with kind: {Kind}", request.Kind);

			var userId = userContext.GetCurrentUser()!.Id;
			var orders = await orderRepository.GetUserOrdersByKind(userId, request.Kind);

			var orderItemsDict = await orderRepository.GetOrderItemsForOrders(orders.Select(o => o.Id).ToList());

			var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

			foreach (var orderDto in orderDtos) //this to take each list<detailedDto> and assign it to it's orderDto
			{
				if (orderItemsDict.TryGetValue(orderDto.Id, out var orderItems))
				{
					orderDto.OrderItems = orderItems;
				}
			}
			return orderDtos;
		}
	}
}
