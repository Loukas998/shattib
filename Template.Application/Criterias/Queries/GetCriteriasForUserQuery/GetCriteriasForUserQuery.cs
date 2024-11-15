using MediatR;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Queries.GetCriteriasForUserQuery;

public class GetCriteriasForUserQuery(string status) : IRequest<IEnumerable<CriteriaDto>>
{
	public string? Status { get; } = status;
}