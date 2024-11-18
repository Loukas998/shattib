using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.Entities.Orders;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Commands.CreateSpecifiedMeasurements
{
	public class CreateSpecifiedMeasurementsCommandHandler(ILogger<CreateSpecifiedMeasurementsCommandHandler> logger,
		IMapper mapper, ISpecifiedMeasurementRepository specifiedMeasurementRepository, IUserContext userContext)
		: IRequestHandler<CreateSpecifiedMeasurementsCommand, int>
	{
		public async Task<int> Handle(CreateSpecifiedMeasurementsCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating Specified Measurement Order: {@Order}", request);
			var specifiedMeas = mapper.Map<SpecifiedMeasurement>(request);
			var currentUser = userContext.GetCurrentUser();
			if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
			var userId = currentUser.Id;
			specifiedMeas.UserId = userId;
			int id = await specifiedMeasurementRepository.CreateAsync(specifiedMeas);
			return id;
		}
	}
}
