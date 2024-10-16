namespace Template.Domain.Entities
{
	public class SubCategory
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public int CategoryId { get; set; } = default!;
	}
}
