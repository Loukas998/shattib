using System.ComponentModel.DataAnnotations;
using Template.Domain.Entities.Products;

namespace Template.Domain.Entities.Orders
{
	public class Order
	{
		public int Id { get; set; }
		public string UserId { get; set; } = default!;
		public float TotalPrice { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateOfOrder { get; set; } 

		public List<Product>? Products { get; set; }
	}
}
