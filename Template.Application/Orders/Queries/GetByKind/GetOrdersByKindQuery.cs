using MediatR;
using Template.Application.Orders.Dtos;

namespace Template.Application.Orders.Queries.GetByKind
{
	public class GetOrdersByKindQuery(string kind) : IRequest<IEnumerable<OrderDto>>
	{
		public string Kind { get; } = kind;
	}
}
