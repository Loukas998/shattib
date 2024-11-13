using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos
{
	public class CatSubCatProductsDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string ImagePath { get; set; } = null;
		public List<SubCategoryDto> SubCategories { get; set; } = default!;
		
	}
}
