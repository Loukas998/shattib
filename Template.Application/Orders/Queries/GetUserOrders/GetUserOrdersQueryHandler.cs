using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetUserOrders
{
	public class GetUserOrdersQueryHandler(ILogger<GetUserOrdersQueryHandler> logger,
		IOrderRepository orderRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderDto>>
	{
		public async Task<IEnumerable<OrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("getting user orders");

			string userId = userContext.GetCurrentUser()!.Id;

			var orders = await orderRepository.GetUserOrders(userId);

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
