using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Template.Application.Products.Dtos;
using Template.Application.Specifications.Dtos;
using Template.Domain.Entities.Products;

namespace Template.Application.Products.Commands.UpdateProductCommand
{
	public class UpdateProductCommand() : IRequest
	{
		public int ProductId { get; set; }
		public int SubCategoryId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Features { get; set; }
		public float? Price { get; set; }
		public float? InstallationTeam { get; set; }
		public string? Brand { get; set; }
		public string? ManufacturingCountry { get; set; }
		public string? Deaf { get; set; }
		public string? RetrivalAndReplacing { get; set; }
		public string? Notes { get; set; }
		public List<ProductMeasurements>? Measurements { get; set; }
		public List<ProductColors>? Colors { get; set; }
		public List<SpecificationDto>? Specifications { get; set; }
	}
}
