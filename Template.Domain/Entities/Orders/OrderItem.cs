namespace Template.Domain.Entities.Orders
{
	public class OrderItem
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public string Quantity { get; set; } = default!;
		public float Price { get; set; }
	}
}
