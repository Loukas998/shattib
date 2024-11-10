using MediatR;
using Template.Application.Consultations.Dtos;

namespace Template.Application.Consultations.Queries.GetUserConsultations
{
	public class GetUserConsultationsQuery : IRequest<IEnumerable<ConsultationDto>>
	{
	}
}
