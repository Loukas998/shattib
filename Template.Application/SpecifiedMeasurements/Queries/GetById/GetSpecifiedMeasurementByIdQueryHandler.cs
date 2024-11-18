using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.SpecifiedMeasurements.Dtos;
using Template.Domain.Entities.Orders;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Queries.GetById
{
	public class GetSpecifiedMeasurementByIdQueryHandler(ILogger<GetSpecifiedMeasurementByIdQueryHandler> logger,
		IMapper mapper, ISpecifiedMeasurementRepository specifiedMeasurementRepository)
		: IRequestHandler<GetSpecifiedMeasurementByIdQuery, SpecifiedMeasurementDto?>
	{
		public async Task<SpecifiedMeasurementDto?> Handle(GetSpecifiedMeasurementByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting Specified Measurement with id: {Id}", request.Id);
			var specM = await specifiedMeasurementRepository.GetByIdAsync(request.Id);
			if (specM == null)
			{
				throw new NotFoundException(nameof(SpecifiedMeasurement), request.Id.ToString());
			}
			var result = mapper.Map<SpecifiedMeasurementDto>(specM);
			return result;
		}
	}
}
