using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using Template.Domain.Entities.Orders;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Commands.UpdateSpecifiedMeasurements
{
	public class UpdateSpecifiedMeasurementsCommandHandler(ILogger<UpdateSpecifiedMeasurementsCommandHandler> logger,
		ISpecifiedMeasurementRepository specifiedMeasurementRepository, IFileService fileService, IMapper mapper)
		: IRequestHandler<UpdateSpecifiedMeasurementsCommand>
	{
		public async Task Handle(UpdateSpecifiedMeasurementsCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Updating Specified Measurement with id: {Id}, the new Specified Measurement: {@Order}",
				request.Id, request);

			var specifiedMeasurement = await specifiedMeasurementRepository.GetByIdAsync(request.Id);
			if (specifiedMeasurement == null)
			{
				throw new NotFoundException(nameof(SpecifiedMeasurement), request.Id.ToString());
			}
			
			string? fileName = Path.GetFileName(specifiedMeasurement.ImagePath);
			if(fileName != null)
			{
				string fullPath = Path.Combine("Images", "SpecifiedMeasurement", fileName).Replace("\\", "/");
				fileService.DeleteFile(fullPath);
			}

			mapper.Map(request, specifiedMeasurement);
			await specifiedMeasurementRepository.SaveChangesAsync();
		}
	}
}
