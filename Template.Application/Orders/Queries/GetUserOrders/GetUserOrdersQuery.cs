using MediatR;
using Template.Application.Orders.Dtos;

namespace Template.Application.Orders.Queries.GetUserOrders
{
	public class GetUserOrdersQuery : IRequest<IEnumerable<OrderDto>>
	{
	}
}
