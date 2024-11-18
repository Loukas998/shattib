using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Criterias.Dtos;
using Template.Application.Users;
using Template.Domain.Exceptions;
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

		var currentUser = userContext.GetCurrentUser();
		if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
		var userId = currentUser.Id;

		var criterias = await criteriaRepository.GetAllByUserId(userId, request.Status);
        return mapper.Map<IEnumerable<CriteriaDto>>(criterias);
    }
}