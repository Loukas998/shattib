using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Consultations.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Queries.GetUserConsultations
{
	public class GetUserConsultationsQueryHandler(ILogger<GetUserConsultationsQueryHandler> logger,
		IMapper mapper, IConsultationRepository consultationRepository, IUserContext userContext) : IRequestHandler<GetUserConsultationsQuery, IEnumerable<ConsultationDto>>
	{
		public async Task<IEnumerable<ConsultationDto>> Handle(GetUserConsultationsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting user consultations");
			var userId = userContext.GetCurrentUser()!.Id;
			var userConsultations = await consultationRepository.GetUserConsultations(userId);
			var results = mapper.Map<IEnumerable<ConsultationDto>>(userConsultations);
			return results;
			
		}
	}
}
