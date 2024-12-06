using Template.Domain.Entities.Orders;

namespace Template.Domain.Entities.Products;

public class Product
{
    public int Id { get; set; }
    public int SubCategoryId { get; set; }
    public string WarehouseCode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public float Price { get; set; }
	public string ManufacturingCountry { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Deaf { get; set; } = default!;
    public string RetrivalAndReplacing { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Keywords { get; set; }
    public float InstallationTeam { get; set; }

    public List<ProductImages> Images { get; set; } = new();
    public List<Specification>? Specifications { get; set; } = [];
    public List<ProductSpecification> ProductSpecifications { get; set; } = [];
    public List<Order>? Orders { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public List<Color> Colors { get; set; } = [];
    public List<Measurement> Measurements { get; set; } = [];

    public SubCategory SubCategory { get; set; }
}