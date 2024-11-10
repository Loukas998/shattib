using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Orders.Dtos;
using Template.Domain.Entities.Orders;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Queries.GetOrderById
{
	public class GetOrderByIdQueryHandler(ILogger<GetOrderByIdQueryHandler> logger,
		IMapper mapper, IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
	{
		public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting the order with id: {OrderId}", request.OrderId);
			var order = await orderRepository.GetOrderById(request.OrderId);
			if (order == null)
			{
				throw new NotFoundException(nameof(Order), request.OrderId.ToString());
			}
			var result = mapper.Map<OrderDto>(order);
			result.OrderItems.Add(await orderRepository.GetItemsForOrder(request.OrderId));
			return result;
		}
	}
}
