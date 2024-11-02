using MediatR;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Commands.CreateCriteriaCommand;

public class CreateCriteriaCommand : IRequest<int>
{
    public string Title { get; set; } = default!;
    public List<CriteriaItemDto> CriteriaItems { get; set; } = default!;
}