using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Criterias.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Queries.GetCriteriasForUserQuery;

public class GetCriteriasForUserQueryHandler(
    ILogger<GetCriteriasForUserQuery> logger,
    IMapper mapper,
    ICriteriaRepository criteriaRepository,
    IUserContext userContext) : IRequestHandler<GetCriteriasForUserQuery, IEnumerable<CriteriaDto>>
{
    public async Task<IEnumerable<CriteriaDto>> Handle(GetCriteriasForUserQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Criterias for User");
        var criterias = await criteriaRepository.GetAllByUserId(userContext.GetCurrentUser()!.Id);
        return mapper.Map<IEnumerable<CriteriaDto>>(criterias);
    }
}