namespace Template.Application.Criterias.Dtos;

public class GetCriteriaBillDto
{
    public int Id { get; set; }
    public string Image { get; set; } = default!;
    public string? Receipt { get; set; }
    public bool? Accepted  { get; set; }
}