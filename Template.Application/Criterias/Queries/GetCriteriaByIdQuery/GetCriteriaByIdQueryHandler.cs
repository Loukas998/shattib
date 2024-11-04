using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Criterias.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Queries.GetCriteriaByIdQuery;

public class GetCriteriaByIdQueryHandler(
    ILogger<GetCriteriaByIdQuery> logger,
    IMapper mapper,
    ICriteriaRepository criteriaRepository) : IRequestHandler<GetCriteriaByIdQuery, CriteriaDto>
{
    public async Task<CriteriaDto> Handle(GetCriteriaByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Criteria with id: {CriteriaId}", request.CriteriaId);
        var criteria = await criteriaRepository.GetByIdAsync(request.CriteriaId);
        if (criteria == null) throw new NotImplementedException();

        var criteriaDto = mapper.Map<CriteriaDto>(criteria);
        return criteriaDto;
    }
}