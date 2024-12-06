namespace Template.Domain.Entities.Products
{
	public class Color
	{
		public int Id { get; set; }
		public string HexCode { get; set; } = default!;
		public float Price { get; set; }
		public string ImagePath { get; set; } = default!;
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
