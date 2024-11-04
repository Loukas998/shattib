using MediatR;
using Template.Application.Consultations.Dtos;

namespace Template.Application.Consultations.Queries.GetConsultationById
{
	public class GetConsultationByIdQuery(int consultationId) : IRequest<ConsultationDto>
	{
		public int ConsultationId { get; } = consultationId;
	}
}
