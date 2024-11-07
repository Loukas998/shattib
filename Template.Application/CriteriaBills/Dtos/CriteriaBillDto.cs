namespace Template.Application.CriteriaBills.Dtos;

public class CriteriaBillDto
{
    public int Id { get; set; }
    public int CriteriaId { get; set; }
    public string Image { get; set; } = default!;
    public string Receipt { get; set; }
    public bool? Accepted { get; set; }
}