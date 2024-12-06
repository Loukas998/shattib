using Template.Domain.Entities.Products;

namespace Template.Application.Products.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;
	public string WarehouseCode { get; set; } = default!;
    public float InstallationTeam { get; set; } = default!;
    public string Brand { get; set; } = default!;
	public string Description { get; set; } = default!;
    public float Price { get; set; }
    public string ManufacturingCountry { get; set; } = default!;
    public string Deaf { get; set; } = default!;
    public string RetrivalAndReplacing { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Keywords { get; set; }
    public List<ProductImages> Images { get; set; } = [];
    public List<MeasurementDto> Measurements { get; set; } = [];
    public List<ProductSpecificationDto> ProductSpecifications { get; set; } = [];
    public List<ColorDto> Colors { get; set; } = [];
}