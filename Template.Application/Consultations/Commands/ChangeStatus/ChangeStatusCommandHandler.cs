using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Commands.ChangeStatus
{
	public class ChangeStatusCommandHandler(ILogger<ChangeStatusCommandHandler> logger,
		IConsultationRepository consultationRepository) : IRequestHandler<ChangeStatusCommand>
	{
		public async Task Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Changing the status of consultation with id: {ConsultationId}: ", request.ConsultationId);
			var consultation = await consultationRepository.GetConsultationByIdAsync(request.ConsultationId);
			if (consultation == null)
			{
				throw new NotFoundException(nameof(Consultation), request.ConsultationId.ToString());
			}
			consultation.Status = request.NewStatus;
			await consultationRepository.SaveChangesAsync();
		}
	}
}
