namespace Template.Application.Products.Dtos
{
	public class SubCategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string ImagePath { get; set; } = null;
		public List<MiniProductDto> Products { get; set; } = [];
	}
}
