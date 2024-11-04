using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Consultations.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Queries.GetAllConsultations
{
	public class GetAllConsultationsQueryHandler(ILogger<GetAllConsultationsQueryHandler> logger,
		IMapper mapper, IConsultationRepository consultationRepository) : IRequestHandler<GetAllConsultationsQuery, IEnumerable<ConsultationDto>>
	{
		public async Task<IEnumerable<ConsultationDto>> Handle(GetAllConsultationsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all the consultations");
			var consultations = await consultationRepository.GetAllConsultationsAsync();
			var results = mapper.Map<IEnumerable<ConsultationDto>>(consultations);
			return results;
		}
	}
}
