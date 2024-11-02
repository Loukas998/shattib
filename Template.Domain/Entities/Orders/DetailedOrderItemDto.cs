namespace Template.Domain.Entities.Orders
{
	public class DetailedOrderItemDto
	{
		public string ProductName { get; set; } = default!;
		public string ProductMainImage { get; set; } = default!;
		public int Quantitiy { get; set; }
		public float TotalPriceForThisProduct { get; set; }
	}
}
