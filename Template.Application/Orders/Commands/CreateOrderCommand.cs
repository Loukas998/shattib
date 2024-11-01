using MediatR;
using System.ComponentModel.DataAnnotations;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Products;

namespace Template.Application.Orders.Commands
{
	public class CreateOrderCommand : IRequest<int>
	{
		[DataType(DataType.Date)]
		public DateTime DateOfOrder { get; set; } = DateTime.Now;
	}
}
