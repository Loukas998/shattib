using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetAllOrders
{
	public class GetAllOrdersQueryHandler(ILogger<GetAllOrdersQueryHandler> logger,
		IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
	{
		public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all orders");

			var orders = await orderRepository.GetAllOrders();

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
			// if you don't get it then dont worry I got this
		}
	}
}
