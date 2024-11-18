using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.SpecifiedMeasurements.Dtos;
using Template.Application.Users;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Queries.GetByUser
{
	public class GetUserSpecifiedMeasurementsQueryHandler(ILogger<GetUserSpecifiedMeasurementsQueryHandler> logger, 
		IMapper mapper, ISpecifiedMeasurementRepository specifiedMeasurementRepository, IUserContext userContext)
		: IRequestHandler<GetUserSpecifiedMeasurementsQuery, IEnumerable<SpecifiedMeasurementDto>>
	{
		public async Task<IEnumerable<SpecifiedMeasurementDto>> Handle(GetUserSpecifiedMeasurementsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting user Specified Measurement");
			var currentUser = userContext.GetCurrentUser();
			if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
			var userId = currentUser.Id;
			var specMs = await specifiedMeasurementRepository.GetAllByUserAsync(userId);
			var result = mapper.Map<IEnumerable<SpecifiedMeasurementDto>>(specMs);
			return result;
		}
	}
}
