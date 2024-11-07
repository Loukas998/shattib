using MediatR;

namespace Template.Application.CriteriaBills.Commands.UpdateCriteriaBillAcceptedCommand;

public class UpdateCriteriaBillAcceptedCommand : IRequest
{
    public int Id { get; set; }
    public bool Accepted { get; set; } = default!;
}