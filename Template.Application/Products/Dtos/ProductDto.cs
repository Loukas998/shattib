﻿using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		public int SubCategoryId { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string Features { get; set; } = default!;
		public float Price { get; set; }
		public string MeasurementUnit { get; set; } = default!;
		public string Meaurements { get; set; } = default!;
		public string ManufacturingCountry { get; set; } = default!;
		public string Color { get; set; } = default!;
		public bool Deaf { get; set; } = default!;
		public bool RetrivalAndReplacing { get; set; } = default!;
		public string? Notes { get; set; }
		public List<ProductImages> ProductImages { get; set; } = [];
	}
}
