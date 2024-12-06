using MediatR;
using System.ComponentModel.DataAnnotations;
using Template.Domain.Constants;

namespace Template.Application.Consultations.Commands.CreateConsultation
{
	public class CreateConsultationCommand : IRequest<int>
	{
		public string PhoneNumber { get; set; } = default!;
		public string ProjectCategory { get; set; } = default!;
		public string Details { get; set; } = default!;
		public string Status { get; set; } = ConsultationConstants.Pending;
		[DataType(DataType.Date)]
		public DateTime DateOfRequest { get; set; } = DateTime.Now;
	}
}
