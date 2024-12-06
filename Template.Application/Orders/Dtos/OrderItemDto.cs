namespace Template.Application.Orders.Dtos
{
	public class OrderItemDto
	{
		public int ColorId { get; set; }
		public int MeasurementId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public bool WithInstallation { get; set; } = default!;
	}
}
