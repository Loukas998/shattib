using Template.Application.Comments.Dtos;

namespace Template.Application.Criterias.Dtos;

public class CriteriaDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;

    public List<GetCriteriaItemDto> CriteriaItems { get; set; }

    public List<CommentDto> Comments { get; set; }

    public List<CriteriaBillsDto> Invoices { get; set; }
}