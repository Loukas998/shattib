using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Specifications.Dtos;
using Template.Domain.Entities.Products;

namespace Template.Application.Products.Commands.CreateProductCommand
{
	public class CreateProductCommand : IRequest<int>
	{
		public int SubCategoryId { get; set; } = default!;
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string Features { get; set; } = default!;
		public float Price { get; set; }
		public string MeasurementUnit { get; set; } = default!;
		public string Meaurements { get; set; } = default!;
		public string ManufacturingCountry { get; set; } = default!;
		public string Color { get; set; } = default!;
		public string Deaf { get; set; } = default!;
		public string RetrivalAndReplacing { get; set; } = default!;
		public string? Notes { get; set; }

		public List<SpecificationDto>? Specifications { get; set; }
		public List<IFormFile> Images { get; set; } = default!;
	}
}
