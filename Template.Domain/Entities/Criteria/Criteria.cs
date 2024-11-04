using Template.Domain.Constants;

namespace Template.Domain.Entities.Criteria;

public class Criteria
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Status { get; set; } = CriteriaStatus.Pending.ToString();
    public List<CriteriaItem> CriteriaItems { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<CriteriaBills> CriteriaBills { get; set; } = new();
}