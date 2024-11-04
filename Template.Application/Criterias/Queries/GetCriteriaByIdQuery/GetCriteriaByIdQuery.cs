using MediatR;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Queries.GetCriteriaByIdQuery;

public class GetCriteriaByIdQuery(int criteriaId) : IRequest<CriteriaDto>
{
    public int CriteriaId { get; } = criteriaId;
}