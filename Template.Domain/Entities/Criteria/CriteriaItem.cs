using Template.Domain.Entities.Products;

namespace Template.Domain.Entities.Criteria;

public class CriteriaItem
{
    public int Id { get; set; }
    public int CriteriaId { get; set; } = default!;
    public int CategoryId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public bool State { get; set; }
    public float Amount { get; set; }
    public string MeasurementUnit { get; set; } = default;
    
    public Criteria Criteria { get; set; }
    public Category Category { get; set; }
    public List<CriteriaImages> CriteriaImages { get; set; }
}