using MediatR;
using Template.Application.Orders.Dtos;

namespace Template.Application.Orders.Queries.GetOrdersByStatus
{
	public class GetOrdersByStatusQuery(string status) : IRequest<IEnumerable<OrderDto>>
	{
		public string Status { get; } = status;
	}
}
