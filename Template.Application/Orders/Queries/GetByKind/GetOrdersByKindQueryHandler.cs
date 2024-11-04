using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetByKind
{
	public class GetOrdersByKindQueryHandler(ILogger<GetOrdersByKindQueryHandler> logger, 
		IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersByKindQuery, IEnumerable<OrderDto>>
	{
		public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByKindQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all orders of kind: {Kind}", request.Kind);

			var orders = await orderRepository.GetOrdersByKind(request.Kind);

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
