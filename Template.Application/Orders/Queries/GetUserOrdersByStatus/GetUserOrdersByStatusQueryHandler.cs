using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetUserOrdersByStatus
{
	public class GetUserOrdersByStatusQueryHandler(ILogger<GetUserOrdersByStatusQueryHandler> logger,
		IMapper mapper, IOrderRepository orderRepository, IUserContext userContext) : IRequestHandler<GetUserOrdersByStatusQuery, IEnumerable<OrderDto>>
	{
		public async Task<IEnumerable<OrderDto>> Handle(GetUserOrdersByStatusQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all user orders with status: {Status}", request.Status);

			string userId = userContext.GetCurrentUser()!.Id;
			var orders = await orderRepository.GetUserOrdersByStatus(request.Status, userId);

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
