using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Criterias;
using Template.Domain.Repositories;

namespace Template.Application.CriteriaBills.Commands.CreateCriteriaBillCommand;

public class CreateCriteriaBillCommandHandler(
    ILogger<CreateCriteriaBillCommandHandler> logger,
    IMapper mapper,
    ICriteriaBillsRepository criteriaBillsRepository)
    : IRequestHandler<CreateCriteriaBillCommand, int>
{
    public async Task<int> Handle(CreateCriteriaBillCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new CriteriaBill {@CriteriaBill}", request);
        var bill = mapper.Map<CriteriaBill>(request);
        return await criteriaBillsRepository.CreateBillAsync(bill, cancellationToken);
    }
}