namespace Template.Application.Criterias.Dtos;

public class CreateCriteriaBillsDto
{
    public int Id { get; set; }
    public string Image { get; set; } = default!;
    public bool Accepted { get; set; }
}