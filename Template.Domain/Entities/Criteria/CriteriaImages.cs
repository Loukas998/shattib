namespace Template.Domain.Entities.Criteria;

public class CriteriaImages
{
    public int Id { get; set; }
    public int CriteriaCategoryId { get; set; } = default!;
    public string ImagePath { get; set; } = default!;
    
    public CriteriaItem CriteriaItem { get; set; }
}