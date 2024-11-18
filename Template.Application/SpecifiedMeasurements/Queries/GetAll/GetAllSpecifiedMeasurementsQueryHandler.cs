using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.SpecifiedMeasurements.Dtos;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Queries.GetAll
{
	public class GetAllSpecifiedMeasurementsQueryHandler(ILogger<GetAllSpecifiedMeasurementsQueryHandler> logger,
		IMapper mapper, ISpecifiedMeasurementRepository specifiedMeasurementRepository)
		: IRequestHandler<GetAllSpecifiedMeasurementsQuery, IEnumerable<SpecifiedMeasurementDto?>>
	{
		public async Task<IEnumerable<SpecifiedMeasurementDto?>> Handle(GetAllSpecifiedMeasurementsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all Specified Measurements");
			var specM = await specifiedMeasurementRepository.GetAllAsync();
			var result = mapper.Map<IEnumerable<SpecifiedMeasurementDto>>(specM);
			return result;
		}
	}
}
