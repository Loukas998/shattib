using MediatR;

namespace Template.Application.Consultations.Commands.DeleteConsultation
{
	public class DeleteConsultationCommand : IRequest
	{
		public int ConsultationId { get; set; } = default!;
	}
}
