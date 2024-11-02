using MediatR;
using Template.Application.Orders.Dtos;

namespace Template.Application.Orders.Queries.GetUserOrdersByKind
{
	public class GetUserOrdersByKindQuery(string kind) : IRequest<IEnumerable<OrderDto>>
	{
		public string Kind { get; }
	}
}
