using Template.Domain.Entities.Criterias;

namespace Template.Domain.Entities.Products;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImagePath { get; set; }
    public List<SubCategory> SubCategories { get; set; }
    public List<CriteriaItem> CriteriaItems { get; set; }
}