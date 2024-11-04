using MediatR;
using Template.Application.Orders.Dtos;

namespace Template.Application.Orders.Queries.GetUserOrdersByStatus
{
	public class GetUserOrdersByStatusQuery(string status) : IRequest<IEnumerable<OrderDto>>
	{
		public string Status { get; } = status;
	}
}
