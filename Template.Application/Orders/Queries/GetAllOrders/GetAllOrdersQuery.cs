using MediatR;
using Template.Application.Orders.Dtos;
namespace Template.Application.Orders.Queries.GetAllOrders
{
	public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
	{
	}
}
