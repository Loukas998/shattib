namespace Template.Domain.Entities.Products
{
	public class ProductImages
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ImagePath { get; set; } = default!;
	}
}
