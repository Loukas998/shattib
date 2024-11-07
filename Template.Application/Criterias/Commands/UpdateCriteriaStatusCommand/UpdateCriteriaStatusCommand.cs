using MediatR;

namespace Template.Application.Criterias.Commands.UpdateCriteriaStatusCommand;

public class UpdateCriteriaStatusCommand : IRequest
{
    public int Id { get; set; }
    public string Status { get; set; } = default!;
}