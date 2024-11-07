using MediatR;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Queries.GetCriteriasForUserQuery;

public class GetCriteriasForUserQuery : IRequest<IEnumerable<CriteriaDto>>
{
}