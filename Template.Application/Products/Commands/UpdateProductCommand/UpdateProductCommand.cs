using MediatR;

namespace Template.Application.Products.Commands.UpdateProductCommand
{
	public class UpdateProductCommand() : IRequest
	{
		public int ProductId { get; set; }
		public int SubCategoryId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Features { get; set; }
		public float Price { get; set; }
		public string? MeasurementUnit { get; set; }
		public string? Meaurements { get; set; }
		public string? ManufacturingCountry { get; set; }
		public string? Color { get; set; }
		public bool Deaf { get; set; }
		public bool RetrivalAndReplacing { get; set; }
		public string? Notes { get; set; }
	}
}
