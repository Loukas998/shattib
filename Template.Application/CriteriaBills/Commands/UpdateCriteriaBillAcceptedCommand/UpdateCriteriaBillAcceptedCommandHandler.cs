using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criteria;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.CriteriaBills.Commands.UpdateCriteriaBillAcceptedCommand;

public class UpdateCriteriaBillAcceptedCommandHandler(
    ILogger<UpdateCriteriaBillAcceptedCommand> logger,
    IMapper mapper,
    ICriteriaBillsRepository criteriaBillsRepository) : IRequestHandler<UpdateCriteriaBillAcceptedCommand>
{
    public async Task Handle(UpdateCriteriaBillAcceptedCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Bill Accepted with CriteriaBillId: {CriteriaBillId}", request.Id);
        var bill = await criteriaBillsRepository.UpdateBillAcceptedAsync(request.Id, request.Accepted);
        if (bill == null) throw new NotFoundException(nameof(CriteriaBill),  request.Id.ToString());
    }
}