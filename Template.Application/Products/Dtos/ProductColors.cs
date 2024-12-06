using Microsoft.AspNetCore.Http;
namespace Template.Application.Products.Dtos
{
	public class ProductColors
	{
		public string HexCode { get; set; } = default!;
		public float Price { get; set; }
		public IFormFile ImagePath { get; set; } = default!;
	}
}
