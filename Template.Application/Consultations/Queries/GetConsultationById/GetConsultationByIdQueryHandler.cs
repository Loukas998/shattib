using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Consultations.Dtos;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Consultations.Queries.GetConsultationById
{
	public class GetConsultationByIdQueryHandler(ILogger<GetConsultationByIdQueryHandler> logger, IMapper mapper,
		IConsultationRepository consultationRepository) : IRequestHandler<GetConsultationByIdQuery, ConsultationDto>
	{
		public async Task<ConsultationDto> Handle(GetConsultationByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting consultation with id: {ConsultationId}", request.ConsultationId);
			var consultation = await consultationRepository.GetConsultationByIdAsync(request.ConsultationId);
			if(consultation == null)
			{
				throw new NotFoundException(nameof(Consultation), request.ConsultationId.ToString());
			}
			var result = mapper.Map<ConsultationDto>(consultation);
			return result;
		}
	}
}
