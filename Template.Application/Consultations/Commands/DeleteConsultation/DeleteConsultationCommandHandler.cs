using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Commands.DeleteConsultation
{
	public class DeleteConsultationCommandHandler(ILogger<DeleteConsultationCommandHandler> logger, 
		IConsultationRepository consultationRepository) : IRequestHandler<DeleteConsultationCommand>
	{
		public async Task Handle(DeleteConsultationCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting consultation with id {ConsultationId}", request.ConsultationId);
			var consultation = await consultationRepository.GetConsultationByIdAsync(request.ConsultationId);
			if (consultation == null)
			{
				throw new NotFoundException(nameof(Consultation), request.ConsultationId.ToString());
			}
			await consultationRepository.DeleteAsync(consultation);
		}
	}
}
