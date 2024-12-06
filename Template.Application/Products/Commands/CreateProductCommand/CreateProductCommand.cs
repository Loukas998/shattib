using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Products.Dtos;
using Template.Application.Specifications.Dtos;

namespace Template.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommand : IRequest<int>
{
    public int SubCategoryId { get; set; } = default!;
    public string WarehouseCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public float Price { get; set; }
	public string ManufacturingCountry { get; set; } = default!;
	public string Brand { get; set; } = default!;
    public string Deaf { get; set; } = default!;
    public string RetrivalAndReplacing { get; set; } = default!;
    public string? Notes { get; set; }
    public float InstallationTeam { get; set; }

    public List<ProductMeasurements> Measurements { get; set; } = [];
	public List<SpecificationDto>? Specifications { get; set; }
    public List<ProductColors> ProductColors { get; set; } = [];
    public List<IFormFile> Images { get; set; } = default!;
}