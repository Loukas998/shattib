namespace Template.Domain.Entities.Criteria;

public class Criteria
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;

    public List<CriteriaItem> CriteriaItems { get; set; }
    public List<Comment> Comments { get; set; }
    public List<CriteriaBills> Invoices { get; set; }
}