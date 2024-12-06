namespace Template.Application.Products.Dtos
{
	public class ColorDto
	{
		public int Id { get; set; }
		public string HexCode { get; set; } = default!;
		public float Price { get; set; }
		public string ImagePath { get; set; } = default!;
	}
}
