namespace Template.Domain.Entities.Products
{
	public class Measurement
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public float Price { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
