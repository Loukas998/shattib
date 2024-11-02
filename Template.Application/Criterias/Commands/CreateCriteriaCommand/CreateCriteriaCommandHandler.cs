using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Commands.CreateCriteriaCommand;

public class CreateCriteriaCommandHandler(
    ILogger<CreateCriteriaCommand> logger,
    IMapper mapper,
    ICriteriaRepository criteriaRepository) : IRequestHandler<CreateCriteriaCommand, int>
{
    public async Task<int> Handle(CreateCriteriaCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new Criteria {@Criteria}", request);
        var criteria = mapper.Map<Criteria>(request);
        return await criteriaRepository.CreateCriteriaAsync(criteria);
    }
}