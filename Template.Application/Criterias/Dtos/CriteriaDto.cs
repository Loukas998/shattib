using System.ComponentModel.DataAnnotations;
using Template.Application.Comments.Dtos;

namespace Template.Application.Criterias.Dtos;

public class CriteriaDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
	[DataType(DataType.Date)]
	public DateTime DateOfCreation { get; set; } = default!;
    public List<GetCriteriaItemDto> CriteriaItems { get; set; }

    public List<CommentDto> Comments { get; set; }

    public List<CreateCriteriaBillsDto> Invoices { get; set; }
}