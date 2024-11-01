using AutoMapper;
using Template.Application.Orders.Commands;
using Template.Domain.Entities.Orders;

namespace Template.Application.Orders.Dtos
{
	public class OrdersProfile : Profile
	{
		public OrdersProfile()
		{
			CreateMap<CreateOrderCommand, Order>();
		}
	}
}
