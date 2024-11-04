using MediatR;
using Template.Application.Consultations.Dtos;

namespace Template.Application.Consultations.Queries.GetAllConsultations
{
	public class GetAllConsultationsQuery : IRequest<IEnumerable<ConsultationDto>>
	{
	}
}
