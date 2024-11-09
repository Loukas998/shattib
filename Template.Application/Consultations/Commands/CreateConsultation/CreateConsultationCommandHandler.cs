using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Commands.CreateConsultation
{
	public class CreateConsultationCommandHandler(ILogger<CreateConsultationCommandHandler> logger,
		IMapper mapper, IConsultationRepository consultationRepository, IUserContext userContext) : IRequestHandler<CreateConsultationCommand, int>
	{
		public async Task<int> Handle(CreateConsultationCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new consultation: {@Consultation}", request);
			var consultation = mapper.Map<Consultation>(request);
			consultation.UserId = userContext.GetCurrentUser()!.Id;
			return await consultationRepository.CreateConsultationAsync(consultation);
		}
	}
}
