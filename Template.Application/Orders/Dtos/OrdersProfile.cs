using AutoMapper;
using Template.Application.Orders.Commands.CreateOrder;
using Template.Domain.Entities.Orders;

namespace Template.Application.Orders.Dtos
{
    public class OrdersProfile : Profile
	{
		public OrdersProfile()
		{
			CreateMap<CreateOrderCommand, Order>();
			CreateMap<Order, OrderDto>();
		}
	}
}
