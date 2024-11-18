using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Template.Domain.Entities.Orders;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Commands.DeleteSpecifiedMeasurements
{
	public class DeleteSpecifiedMeasurementsHandler(ILogger<DeleteSpecifiedMeasurementsHandler> logger,
		ISpecifiedMeasurementRepository specifiedMeasurementRepository)
		: IRequestHandler<DeleteSpecifiedMeasurementsCommand>
	{
		public async Task Handle(DeleteSpecifiedMeasurementsCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting Specified Measurement with id: {Id}", request.Id);
			var specM = await specifiedMeasurementRepository.GetByIdAsync(request.Id);
			if(specM == null)
			{
				throw new NotFoundException(nameof(SpecifiedMeasurement), request.Id.ToString());
			}
			await specifiedMeasurementRepository.DeleteAsync(specM);
		}
	}
}
