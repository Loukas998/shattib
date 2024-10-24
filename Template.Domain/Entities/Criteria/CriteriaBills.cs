namespace Template.Domain.Entities.Criteria;

public class CriteriaBills
{
    public int Id { get; set; }
    public string Image { get; set; } = default!;
    public int CriteriaId { get; set; } = default!;
    public bool Accepted { get; set; }

    public Criteria Criteria { get; set; }
}