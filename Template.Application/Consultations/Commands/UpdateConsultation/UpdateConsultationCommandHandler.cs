using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Commands.UpdateConsultation
{
	public class UpdateConsultationCommandHandler(ILogger<UpdateConsultationCommandHandler> logger,
		IMapper mapper, IConsultationRepository consultationRepository) : IRequestHandler<UpdateConsultationCommand>
	{
		public async Task Handle(UpdateConsultationCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Updating consultation with id:{ConsultationId} with new info:{NewConsultation}",
				request.ConsultationId, request);

			var consultation = await consultationRepository.GetConsultationByIdAsync(request.ConsultationId);
			if (consultation == null)
			{
				throw new NotFoundException(nameof(Consultation), request.ConsultationId.ToString());
			}
			mapper.Map(request, consultation);
			await consultationRepository.SaveChangesAsync();
		}
	}
}
