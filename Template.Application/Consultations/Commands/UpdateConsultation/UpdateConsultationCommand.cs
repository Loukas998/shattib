using MediatR;

namespace Template.Application.Consultations.Commands.UpdateConsultation
{
	public class UpdateConsultationCommand : IRequest
	{
		public int ConsultationId { get; set; }
		public string PhoneNumber { get; set; } = default!;
		public string ConsultationTopic { get; set; } = default!;
		public string EngineerSpecification { get; set; } = default!;
		public string ProjectCategory { get; set; } = default!;
	}
}
