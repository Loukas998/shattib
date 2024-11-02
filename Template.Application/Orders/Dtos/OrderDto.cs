using System.ComponentModel.DataAnnotations;
using Template.Application.Products.Dtos;
using Template.Domain.Entities.Orders;
using Template.Domain.Entities.Products;

namespace Template.Application.Orders.Dtos
{
	public class OrderDto
	{
		public int Id { get; set; }
		public float TotalPrice { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateOfOrder { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DateOfArrival { get; set; }
		public string Status { get; set; } = default!;

		public List<DetailedOrderItemDto> OrderItems { get; set; } = [];
	}
}
