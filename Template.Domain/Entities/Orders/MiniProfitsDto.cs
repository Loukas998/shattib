using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Entities.Orders
{
	public class MiniProfitsDto
	{
		[DataType(DataType.Date)]
		public DateTime DateOfOrder { get; set; }
		public string Username { get; set; } = default!;
		public float TotalPrice { get; set; }
	}
}
