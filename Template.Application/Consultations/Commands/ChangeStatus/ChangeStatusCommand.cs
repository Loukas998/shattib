using MediatR;

namespace Template.Application.Consultations.Commands.ChangeStatus
{
	public class ChangeStatusCommand(int consultationId) : IRequest
	{
		public int ConsultationId { get; } = consultationId;
		public string NewStatus { get; set; } = default!;
	}
}
