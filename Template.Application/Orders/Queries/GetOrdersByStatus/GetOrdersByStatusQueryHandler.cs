using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetOrdersByStatus
{
	public class GetOrdersByStatusQueryHandler(ILogger<GetOrdersByStatusQueryHandler> logger,
		IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersByStatusQuery, IEnumerable<OrderDto>>
	{
		public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByStatusQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("getting all orders with status: {Status}", request.Status);

			var orders = await orderRepository.GetOrdersByStatus(request.Status);

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
