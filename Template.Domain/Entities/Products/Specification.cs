namespace Template.Domain.Entities.Products
{
	public class Specification
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;

		public List<Product> Products { get; set; } = [];
		public List<Specification>? Specifications { get; set; } = [];
		public List<ProductSpecification> ProductSpecifications { get; set; } = [];
	}
}
