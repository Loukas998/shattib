using MediatR;
using System.ComponentModel.DataAnnotations;
using Template.Application.Orders.Dtos;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Products;

namespace Template.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        [DataType(DataType.Date)]
        public DateTime DateOfOrder { get; set; } = DateTime.Now;
		public List<OrderItemDto> Items { get; set; } = default!;
        public string Kind { get; set; } = default!;
	}
}
