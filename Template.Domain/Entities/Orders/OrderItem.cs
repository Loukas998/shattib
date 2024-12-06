namespace Template.Domain.Entities.Orders
{
	public class OrderItem
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public string Color { get; set; } = default!;
		public int Quantity { get; set; }
		public float Price { get; set; }
		public bool WithInstallation { get; set; } = default!;
	}
}
