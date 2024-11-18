using MediatR;

namespace Template.Application.Criterias.Commands.DeleteCriteria;

public class DeleteCriteriaCommand(int criteriaId) : IRequest
{
    public int CriteriaId { get; } = criteriaId;
}