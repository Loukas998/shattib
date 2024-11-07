using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.CriteriaBills.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.CriteriaBills.Queries.GetCriteriaBillByIdQuery;

public class GetCriteriaBillByIdQueryHandler(
    ILogger<GetCriteriaBillByIdQuery> logger,
    IMapper mapper,
    ICriteriaBillsRepository criteriaBillsRepository) : IRequestHandler<GetCriteriaBillByIdQuery, CriteriaBillDto>
{
    public async Task<CriteriaBillDto> Handle(GetCriteriaBillByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Bill with ID: {CriteriaBillId}", request.Id);
        var bill = await criteriaBillsRepository.GetBillByIdAsync(request.Id);
        if (bill == null) throw new NotImplementedException();
        var billDto = mapper.Map<CriteriaBillDto>(bill);
        return billDto;
    }
}