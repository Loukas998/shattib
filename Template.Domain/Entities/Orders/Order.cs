using System.ComponentModel.DataAnnotations;
using Template.Domain.Constants;
using Template.Domain.Entities.Products;

namespace Template.Domain.Entities.Orders
{
	public class Order
	{
		public int Id { get; set; }
		public string UserId { get; set; } = default!;
		public float TotalPrice { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DateOfOrder { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DateOfArrival { get; set; }
		public string Status { get; set; } = OrderConstants.Pending;
		public string Kind { get; set; } = default!;

		public List<Product> Products { get; set; } = [];
		public List<OrderItem> OrderItems { get; set; } = [];
	}
}
