using MediatR;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Queries.GetAllCriterias
{
	public class GetAllCriteriasQuery : IRequest<IEnumerable<CriteriaDto>>
	{

	}
}
