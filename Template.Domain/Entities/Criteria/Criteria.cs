namespace Template.Domain.Entities.Criteria;

public class Criteria
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;

    public List<CriteriaItem> CriteriaItems { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
	public List<CriteriaBills> Invoices { get; set; } = new();
}