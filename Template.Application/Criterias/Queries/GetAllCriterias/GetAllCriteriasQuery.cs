using MediatR;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Queries.GetAllCriterias
{
	public class GetAllCriteriasQuery(string status) : IRequest<IEnumerable<CriteriaDto>>
	{
		public string? Status { get; } = status;
	}
}
