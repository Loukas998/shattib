using System.ComponentModel.DataAnnotations;
using Template.Domain.Constants;

namespace Template.Domain.Entities.Criterias;

public class Criteria
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Status { get; set; } = CriteriaStatus.Pending.ToString();
	[DataType(DataType.Date)]
	public DateTime DateOfCreation { get; set; } = default!;
	public List<CriteriaItem> CriteriaItems { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<CriteriaBill> CriteriaBills { get; set; } = new();
    public User User { get; set; } = default!;
    
}