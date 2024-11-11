using System.ComponentModel.DataAnnotations;
using Template.Domain.Constants;

namespace Template.Domain.Entities.EngConsultation
{
	public class Consultation
	{
		public int Id { get; set; }
		public string UserId { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string ConsultationTopic { get; set; } = default!;
		public string ProjectCategory { get; set; } = default!;
		public string Details { get; set; } = default!;
		[DataType(DataType.Date)]
		public DateTime DateOfRequest { get; set; } = default!;
		public string Status { get; set; } = ConsultationConstants.Pending;
		public User User { get; set; } = default!;
	}
}
