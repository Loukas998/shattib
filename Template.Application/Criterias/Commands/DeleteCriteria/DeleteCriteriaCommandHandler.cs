using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criterias;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Criterias.Commands.DeleteCriteria;

public class DeleteCriteriaCommandHandler(ILogger<DeleteCriteriaCommand> logger, ICriteriaRepository criteriaRepository)
    : IRequestHandler<DeleteCriteriaCommand>
{
    public async Task Handle(DeleteCriteriaCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete criteria with id: {CriteriaId}", request.CriteriaId);
        var criteria = await criteriaRepository.GetByIdAsync(request.CriteriaId);
        if (criteria == null) throw new NotFoundException(nameof(Criteria), request.CriteriaId.ToString());
        await criteriaRepository.DeleteCriteriaAsync(criteria);
    }
}