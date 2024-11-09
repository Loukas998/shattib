using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criterias;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Commands.UpdateCriteriaStatusCommand;

public class UpdateCriteriaStatusCommandHandler(
    ILogger<UpdateCriteriaStatusCommand> logger,
    IMapper mapper,
    ICriteriaRepository criteriaRepository) : IRequestHandler<UpdateCriteriaStatusCommand>
{
    public async Task Handle(UpdateCriteriaStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Criteria Status with CriteriaId: {CriteriaId}, New Status {Status}", request.Id,
            request.Status);
        var criteria = await criteriaRepository.UpdateCriteriaStatusAsync(request.Id, request.Status);
        if (criteria == null) throw new NotFoundException(nameof(Criteria), request.Id.ToString());
    }
}