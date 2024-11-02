using MediatR;
using Template.Application.Orders.Dtos;

namespace Template.Application.Orders.Queries.GetOrderById
{
	public class GetOrderByIdQuery(int orderId) : IRequest<OrderDto>
	{
		public int OrderId { get; } = orderId;
	}
}
