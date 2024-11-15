using MediatR;

namespace Template.Application.Consultations.Commands.ChangeStatus
{
	public class ChangeStatusCommand() : IRequest
	{
		public int ConsultationId { get; set; }
		public string NewStatus { get; set; } = default!;
	}
}
