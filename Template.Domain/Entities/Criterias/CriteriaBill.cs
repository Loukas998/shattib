namespace Template.Domain.Entities.Criterias;

public class CriteriaBill
{
    public int Id { get; set; }
    public string Image { get; set; } = default!;
    public string? Receipt { get; set; }
    public int CriteriaId { get; set; } = default!;
    public bool? Accepted { get; set; }

    public Criteria Criteria { get; set; }
}