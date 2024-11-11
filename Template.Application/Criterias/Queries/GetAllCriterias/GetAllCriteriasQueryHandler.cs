using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Criterias.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Queries.GetAllCriterias
{
	public class GetAllCriteriasQueryHandler(ILogger<GetAllCriteriasQueryHandler> logger,
		IMapper mapper, ICriteriaRepository criteriaRepository) : IRequestHandler<GetAllCriteriasQuery, IEnumerable<CriteriaDto>>
	{
		public async Task<IEnumerable<CriteriaDto>> Handle(GetAllCriteriasQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all criterias");

			var criterias = await criteriaRepository.GetAllAsync(request.Status);
			var results = mapper.Map<IEnumerable<CriteriaDto>>(criterias);
			return results;
		}
	}
}
