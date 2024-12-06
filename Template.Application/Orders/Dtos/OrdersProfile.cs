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
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
		}
	}
}
